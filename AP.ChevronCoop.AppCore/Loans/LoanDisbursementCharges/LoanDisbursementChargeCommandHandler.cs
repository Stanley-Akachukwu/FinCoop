using AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanDisbursements;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanDisbursementCharges;

public class LoanDisbursementChargeCommandHandler :
  IRequestHandler<QueryLoanDisbursementChargeCommand, CommandResult<IQueryable<LoanDisbursementCharge>>>,
  IRequestHandler<CreateLoanDisbursementChargeCommand, CommandResult<LoanDisbursementChargeViewModel>>,
  IRequestHandler<UpdateLoanDisbursementChargeCommand, CommandResult<LoanDisbursementChargeViewModel>>,
  IRequestHandler<DeleteLoanDisbursementChargeCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public LoanDisbursementChargeCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<LoanDisbursementChargeCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }

  public async Task<CommandResult<LoanDisbursementChargeViewModel>> Handle(CreateLoanDisbursementChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanDisbursementChargeViewModel>();
    var entity = mapper.Map<LoanDisbursementCharge>(request);

    dbContext.LoanDisbursementCharges.Add(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanDisbursementChargeViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteLoanDisbursementChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.LoanDisbursementCharges.FindAsync(request.Id);

    dbContext.LoanDisbursementCharges.Remove(entity!);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }

  public Task<CommandResult<IQueryable<LoanDisbursementCharge>>> Handle(QueryLoanDisbursementChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<IQueryable<LoanDisbursementCharge>>();
    rsp.Response = dbContext.LoanDisbursementCharges;

    return Task.FromResult(rsp);
  }

  public async Task<CommandResult<LoanDisbursementChargeViewModel>> Handle(UpdateLoanDisbursementChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanDisbursementChargeViewModel>();
    var entity = await dbContext.LoanDisbursementCharges.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.LoanDisbursementCharges.Update(entity!);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanDisbursementChargeViewModel>(entity);

    return rsp;
  }
}