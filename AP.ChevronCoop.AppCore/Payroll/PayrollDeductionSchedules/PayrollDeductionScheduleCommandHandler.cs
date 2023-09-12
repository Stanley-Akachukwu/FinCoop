using AP.ChevronCoop.AppCore.Services.BackgroundServices;
using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedule;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionSchedules;

public class PayrollDeductionScheduleCommandHandler :
  IRequestHandler<QueryPayrollDeductionScheduleCommand, CommandResult<IQueryable<Entities.Payroll.PayrollDeductionSchedules.PayrollDeductionSchedule>>>,
  IRequestHandler<CreatePayrollDeductionScheduleCommand, CommandResult<PayrollDeductionScheduleViewModel>>,
  IRequestHandler<UpdatePayrollDeductionScheduleCommand, CommandResult<PayrollDeductionScheduleViewModel>>,
  IRequestHandler<DeletePayrollDeductionScheduleCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly IPayrollScheduleBackgroundService _payrollScheduleBackgroundService;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public PayrollDeductionScheduleCommandHandler(ChevronCoopDbContext appDbContext,
      ILogger<PayrollDeductionScheduleCommandHandler> _logger, IMapper _mapper, IPayrollScheduleBackgroundService payrollScheduleBackgroundService)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _payrollScheduleBackgroundService = payrollScheduleBackgroundService;
    }


    public async Task<CommandResult<PayrollDeductionScheduleViewModel>> Handle(
      CreatePayrollDeductionScheduleCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<PayrollDeductionScheduleViewModel>();

        var entity = mapper.Map<Entities.Payroll.PayrollDeductionSchedules.PayrollDeductionSchedule>(request);

        dbContext.PayrollDeductionSchedules.Add(entity);
        await dbContext.SaveChangesAsync();

        await _payrollScheduleBackgroundService.CreateScheduledJobs(entity);

        rsp.Response = mapper.Map<PayrollDeductionScheduleViewModel>(entity);

        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeletePayrollDeductionScheduleCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await dbContext.PayrollDeductionSchedules.FindAsync(request.Id);

        dbContext.PayrollDeductionSchedules.Remove(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = "Data successfully deleted";

        return rsp;
    }


    public Task<CommandResult<IQueryable<Entities.Payroll.PayrollDeductionSchedules.PayrollDeductionSchedule>>> Handle(QueryPayrollDeductionScheduleCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<Entities.Payroll.PayrollDeductionSchedules.PayrollDeductionSchedule>>();
        rsp.Response = dbContext.PayrollDeductionSchedules;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<PayrollDeductionScheduleViewModel>> Handle(
      UpdatePayrollDeductionScheduleCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<PayrollDeductionScheduleViewModel>();
        var entity = await dbContext.PayrollDeductionSchedules.FindAsync(request.Id);

        mapper.Map(request, entity);

        dbContext.PayrollDeductionSchedules.Update(entity);
        await dbContext.SaveChangesAsync();

        rsp.Response = mapper.Map<PayrollDeductionScheduleViewModel>(entity);

        return rsp;
    }
}