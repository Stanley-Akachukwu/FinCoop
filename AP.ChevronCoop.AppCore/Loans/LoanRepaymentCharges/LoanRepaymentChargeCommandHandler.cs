using AP.ChevronCoop.AppDomain.Loans.LoanRepaymentCharges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanRepayment;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepaymentCharges;

public class LoanRepaymentChargeCommandHandler :
  IRequestHandler<QueryLoanRepaymentChargeCommand, CommandResult<IQueryable<LoanRepaymentCharge>>>,
  IRequestHandler<CreateLoanRepaymentChargeCommand, CommandResult<LoanRepaymentChargeViewModel>>,
  IRequestHandler<UpdateLoanRepaymentChargeCommand, CommandResult<LoanRepaymentChargeViewModel>>,
  IRequestHandler<DeleteLoanRepaymentChargeCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public LoanRepaymentChargeCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<LoanRepaymentChargeCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }

  public async Task<CommandResult<LoanRepaymentChargeViewModel>> Handle(CreateLoanRepaymentChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanRepaymentChargeViewModel>();
    var entity = mapper.Map<LoanRepaymentCharge>(request);

    dbContext.LoanRepaymentCharges.Add(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanRepaymentChargeViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteLoanRepaymentChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.LoanRepaymentCharges.FindAsync(request.Id);

    dbContext.LoanRepaymentCharges.Remove(entity!);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }

  public Task<CommandResult<IQueryable<LoanRepaymentCharge>>> Handle(QueryLoanRepaymentChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<IQueryable<LoanRepaymentCharge>>();
    rsp.Response = dbContext.LoanRepaymentCharges;

    return Task.FromResult(rsp);
  }

  public async Task<CommandResult<LoanRepaymentChargeViewModel>> Handle(UpdateLoanRepaymentChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanRepaymentChargeViewModel>();
    var entity = await dbContext.LoanRepaymentCharges.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.LoanRepaymentCharges.Update(entity!);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanRepaymentChargeViewModel>(entity);

    return rsp;
  }
}