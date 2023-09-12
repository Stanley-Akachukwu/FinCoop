using AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopupCharges;

public class LoanTopupChargeCommandHandler :
  IRequestHandler<QueryLoanTopupChargeCommand, CommandResult<IQueryable<LoanTopupCharge>>>,
  IRequestHandler<CreateLoanTopupChargeCommand, CommandResult<LoanTopupChargeViewModel>>,
  IRequestHandler<UpdateLoanTopupChargeCommand, CommandResult<LoanTopupChargeViewModel>>,
  IRequestHandler<DeleteLoanTopupChargeCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public LoanTopupChargeCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<LoanTopupChargeCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }

  public async Task<CommandResult<LoanTopupChargeViewModel>> Handle(CreateLoanTopupChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanTopupChargeViewModel>();
    var entity = mapper.Map<LoanTopupCharge>(request);

    dbContext.LoanTopupCharges.Add(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanTopupChargeViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteLoanTopupChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.LoanTopupCharges.FindAsync(request.Id);

    dbContext.LoanTopupCharges.Remove(entity!);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }

  public Task<CommandResult<IQueryable<LoanTopupCharge>>> Handle(QueryLoanTopupChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<IQueryable<LoanTopupCharge>>();
    rsp.Response = dbContext.LoanTopupCharges;

    return Task.FromResult(rsp);
  }

  public async Task<CommandResult<LoanTopupChargeViewModel>> Handle(UpdateLoanTopupChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanTopupChargeViewModel>();
    var entity = await dbContext.LoanTopupCharges.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.LoanTopupCharges.Update(entity!);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanTopupChargeViewModel>(entity);

    return rsp;
  }
}