using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Members.MemberBankAccounts;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBankAccounts;

public class MemberBankAccountCommandHandler :
  IRequestHandler<QueryMemberBankAccountCommand, CommandResult<IQueryable<MemberBankAccount>>>,
  IRequestHandler<CreateMemberBankAccountCommand, CommandResult<MemberBankAccountViewModel>>,
  IRequestHandler<UpdateMemberBankAccountCommand, CommandResult<MemberBankAccountViewModel>>,
  IRequestHandler<DeleteMemberBankAccountCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public MemberBankAccountCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<MemberBankAccountCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }

    public async Task<CommandResult<MemberBankAccountViewModel>> Handle(CreateMemberBankAccountCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<MemberBankAccountViewModel>();
        var entity = mapper.Map<MemberBankAccount>(request);
        dbContext.MemberBankAccounts.Add(entity);
        
        var currency = dbContext.Currencies.FirstOrDefault(x => x.Code.ToLower() == "ngn");
        var memberProfile = dbContext.MemberProfiles.FirstOrDefault(profile => profile.Id == request.ProfileId);
        var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.ApplicationUserId == memberProfile.ApplicationUserId, cancellationToken: cancellationToken);
        if (customer != null)
        {
          var customerAccountMap = mapper.Map<CustomerBankAccount>(entity);
          customerAccountMap.CustomerId = customer.Id;
          
          var ledgerAccount = new LedgerAccount();
          ledgerAccount.Code = NHiloHelper.GetNextKey(nameof(LedgerAccount)).ToString();
          ledgerAccount.Name = $"Customer Bank Account ({customer.CustomerNo}-{customerAccountMap.AccountNumber})";
          ledgerAccount.IsOfficeAccount = true;
          ledgerAccount.AccountType = COAType.CONTROL;
          ledgerAccount.AllowManualEntry = true;
          ledgerAccount.CurrencyId = currency.Id;
          dbContext.LedgerAccounts.Add(ledgerAccount);

          customerAccountMap.LedgerAccount = ledgerAccount;
          // dbContext.Customers.Add(entity);
          dbContext.CustomerBankAccounts.Add(customerAccountMap);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<MemberBankAccountViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteMemberBankAccountCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.MemberBankAccounts.FindAsync(request.Id);

        entity!.IsActive = true;
        entity.IsDeleted = true;

        dbContext.MemberBankAccounts.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = "Data successfully deleted";

        return rsp;
    }


    public Task<CommandResult<IQueryable<MemberBankAccount>>> Handle(QueryMemberBankAccountCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<MemberBankAccount>>();
        rsp.Response = dbContext.MemberBankAccounts;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<MemberBankAccountViewModel>> Handle(UpdateMemberBankAccountCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<MemberBankAccountViewModel>();
        var entity = await dbContext.MemberBankAccounts.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.MemberBankAccounts.Update(entity!);
        await dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = mapper.Map<MemberBankAccountViewModel>(entity);

        return rsp;
    }
}