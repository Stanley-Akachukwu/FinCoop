using AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanProductCharges;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanProductCharges;

public class LoanProductChargeCommandHandler :
  IRequestHandler<QueryLoanProductChargeCommand, CommandResult<IQueryable<LoanProductCharge>>>,
  IRequestHandler<CreateLoanProductChargeCommand, CommandResult<LoanProductChargeViewModel>>,
  IRequestHandler<UpdateLoanProductChargeCommand, CommandResult<LoanProductChargeViewModel>>,
  IRequestHandler<DeleteLoanProductChargeCommand, CommandResult<string>>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public LoanProductChargeCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<LoanProductChargeCommandHandler> _logger, IMapper _mapper)
  {
    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;
  }


  public async Task<CommandResult<LoanProductChargeViewModel>> Handle(CreateLoanProductChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanProductChargeViewModel>();
    var entity = mapper.Map<LoanProductCharge>(request);

    dbContext.LoanProductCharges.Add(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanProductChargeViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeleteLoanProductChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.LoanProductCharges.FindAsync(request.Id);

    dbContext.LoanProductCharges.Remove(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = "Data successfully deleted";

    return rsp;
  }


  public Task<CommandResult<IQueryable<LoanProductCharge>>> Handle(QueryLoanProductChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<IQueryable<LoanProductCharge>>();
    rsp.Response = dbContext.LoanProductCharges;

    return Task.FromResult(rsp);
  }

  public async Task<CommandResult<LoanProductChargeViewModel>> Handle(UpdateLoanProductChargeCommand request,
    CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<LoanProductChargeViewModel>();
    var entity = await dbContext.LoanProductCharges.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.LoanProductCharges.Update(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<LoanProductChargeViewModel>(entity);

    return rsp;
  }
}