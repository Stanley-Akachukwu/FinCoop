using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionScheduleItem;

public class PayrollDeductionScheduleItemCommandHandler :
  IRequestHandler<QueryPayrollDeductionScheduleItemCommand, CommandResult<IQueryable<Entities.Payroll.PayrollDeductionScheduleItem>>>,
  IRequestHandler<CreatePayrollDeductionScheduleItemCommand, CommandResult<PayrollDeductionScheduleItemViewModel>>,
  IRequestHandler<UpdatePayrollDeductionScheduleItemCommand, CommandResult<PayrollDeductionScheduleItemViewModel>>,
  IRequestHandler<DeletePayrollDeductionScheduleItemCommand, CommandResult<string>>
{

  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger logger;
  private readonly IMapper mapper;

  public PayrollDeductionScheduleItemCommandHandler(ChevronCoopDbContext appDbContext,
    ILogger<PayrollDeductionScheduleItemCommandHandler> _logger, IMapper _mapper)
  {

    dbContext = appDbContext;
    logger = _logger;
    mapper = _mapper;

  }


  public Task<CommandResult<IQueryable<Entities.Payroll.PayrollDeductionScheduleItem>>> Handle(QueryPayrollDeductionScheduleItemCommand request, CancellationToken cancellationToken)
  {

    var rsp = new CommandResult<IQueryable<Entities.Payroll.PayrollDeductionScheduleItem>>();
    rsp.Response = dbContext.PayrollDeductionScheduleItems;

    return Task.FromResult(rsp);
  }
		



  public async Task<CommandResult<PayrollDeductionScheduleItemViewModel>> Handle(CreatePayrollDeductionScheduleItemCommand request, CancellationToken cancellationToken)
  {

    var rsp = new CommandResult<PayrollDeductionScheduleItemViewModel>();
    var entity = mapper.Map<Entities.Payroll.PayrollDeductionScheduleItem>(request);

    dbContext.PayrollDeductionScheduleItems.Add(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<PayrollDeductionScheduleItemViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<PayrollDeductionScheduleItemViewModel>> Handle(UpdatePayrollDeductionScheduleItemCommand request, CancellationToken cancellationToken)
  {

    var rsp = new CommandResult<PayrollDeductionScheduleItemViewModel>();
    var entity = await dbContext.PayrollDeductionScheduleItems.FindAsync(request.Id);

    mapper.Map(request, entity);

    dbContext.PayrollDeductionScheduleItems.Update(entity);
    await dbContext.SaveChangesAsync();

    rsp.Response = mapper.Map<PayrollDeductionScheduleItemViewModel>(entity);

    return rsp;
  }

  public async Task<CommandResult<string>> Handle(DeletePayrollDeductionScheduleItemCommand request, CancellationToken cancellationToken)
  {
    var rsp = new CommandResult<string>();
    var entity = await dbContext.PayrollDeductionScheduleItems.FindAsync(request.Id);

    dbContext.PayrollDeductionScheduleItems.Remove(entity);
    await dbContext.SaveChangesAsync();
           
    rsp.Response = "Data successfully deleted";

    return rsp;
  }
}