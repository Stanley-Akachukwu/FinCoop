using AP.ChevronCoop.AppDomain.Loans.LoanOffsetCharges;
using AP.ChevronCoop.AppDomain.Loans.LoanOffSetCharges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSetCharges;

public class LoanOffSetChargeCommandHandler :
  IRequestHandler<QueryLoanOffSetChargeCommand, CommandResult<IQueryable<LoanOffSetCharge>>>,
  IRequestHandler<CreateLoanOffSetChargeCommand, CommandResult<LoanOffSetChargeViewModel>>,
  IRequestHandler<UpdateLoanOffSetChargeCommand, CommandResult<LoanOffSetChargeViewModel>>,
  IRequestHandler<DeleteLoanOffSetChargeCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public LoanOffSetChargeCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<LoanOffSetChargeCommandHandler> _logger, IMapper _mapper)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
    }

    public async Task<CommandResult<LoanOffSetChargeViewModel>> Handle(CreateLoanOffSetChargeCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanOffSetChargeViewModel>();
        var entity = mapper.Map<LoanOffSetCharge>(request);

        dbContext.LoanOffSetCharges.Add(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<LoanOffSetChargeViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteLoanOffSetChargeCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.LoanOffSetCharges.FindAsync(request.Id);

        dbContext.LoanOffSetCharges.Remove(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanOffSetCharge>>> Handle(QueryLoanOffSetChargeCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanOffSetCharge>>();
        rsp.Response = dbContext.LoanOffSetCharges;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<LoanOffSetChargeViewModel>> Handle(UpdateLoanOffSetChargeCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanOffSetChargeViewModel>();
        var entity = await dbContext.LoanOffSetCharges.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.LoanOffSetCharges.Update(entity!);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<LoanOffSetChargeViewModel>(entity);

        return rsp;
    }
}