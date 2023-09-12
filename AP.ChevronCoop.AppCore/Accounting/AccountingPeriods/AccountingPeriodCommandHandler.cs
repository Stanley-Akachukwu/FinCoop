using AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.AccountingPeriods;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.AccountingPeriods
{
    public class AccountingPeriodCommandHandler :
     IRequestHandler<QueryAccountingPeriodCommand, CommandResult<IQueryable<AccountingPeriod>>>,
    IRequestHandler<CreateAccountingPeriodCommand, CommandResult<AccountingPeriodViewModel>>,
    IRequestHandler<UpdateAccountingPeriodCommand, CommandResult<AccountingPeriodViewModel>>,
    IRequestHandler<DeleteAccountingPeriodCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public AccountingPeriodCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<AccountingPeriodCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<AccountingPeriod>>> Handle(QueryAccountingPeriodCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<AccountingPeriod>>();
            rsp.Response = dbContext.AccountingPeriods;

            return Task.FromResult(rsp);
        }




        public async Task<CommandResult<AccountingPeriodViewModel>> Handle(CreateAccountingPeriodCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<AccountingPeriodViewModel>();
            var entity = mapper.Map<AccountingPeriod>(request);

            dbContext.AccountingPeriods.Add(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<AccountingPeriodViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<AccountingPeriodViewModel>> Handle(UpdateAccountingPeriodCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<AccountingPeriodViewModel>();
            var entity = await dbContext.AccountingPeriods.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.AccountingPeriods.Update(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = mapper.Map<AccountingPeriodViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteAccountingPeriodCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.AccountingPeriods.FindAsync(request.Id);

            dbContext.AccountingPeriods.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }








}
