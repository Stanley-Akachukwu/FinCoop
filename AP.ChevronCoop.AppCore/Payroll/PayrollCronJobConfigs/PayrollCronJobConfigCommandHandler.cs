using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollCronJobConfigs
{

    public class PayrollCronJobConfigCommandHandler :
        IRequestHandler<CreatePayrollCronJobConfigCommand, CommandResult<PayrollCronJobConfigViewModel>>,
        IRequestHandler<UpdatePayrollCronJobConfigCommand, CommandResult<PayrollCronJobConfigViewModel>>,
        IRequestHandler<DeletePayrollCronJobConfigCommand, CommandResult<string>>
    {
        private readonly ILoggerService _logger;
        private readonly ChevronCoopDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManageApprovalService _approvalLog;

        public PayrollCronJobConfigCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator,
            UserManager<ApplicationUser> userManager,
            ILoggerService logger, IMapper _mapper, IManageApprovalService approvalLog)
        {
            dbContext = appDbContext;
            _logger = logger;
            mapper = _mapper;
            _mediator = mediator;
            _userManager = userManager;
            _approvalLog = approvalLog;
        }
         
        public async Task<CommandResult<PayrollCronJobConfigViewModel>> Handle(CreatePayrollCronJobConfigCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<PayrollCronJobConfigViewModel>();
            var entity = mapper.Map<PayrollCronJobConfig>(request);

            entity.ComputationStartDate = request.ProcessingDate;
            entity.ComputationEndDate = request.ProcessingEndDate;
            entity.Description = $"{entity.JobName}- Processing Date - {request.ProcessingDate.ToString("dddd, dd MMMM yyyy")}";
         
            await dbContext.PayrollCronJobConfigs.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            rsp.Message = "Job successfully created";
            rsp.Response = mapper.Map<PayrollCronJobConfigViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<PayrollCronJobConfigViewModel>> Handle(UpdatePayrollCronJobConfigCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<PayrollCronJobConfigViewModel>();
            var entity = await dbContext.PayrollCronJobConfigs.FindAsync(request.Id);
            mapper.Map(request, entity);

            entity.ComputationStartDate = request.ProcessingDate;
            entity.ComputationEndDate = request.ProcessingEndDate;
            entity.Description = $"{entity.JobName}- Processing Date - {request.ProcessingDate.ToString("dddd, dd MMMM yyyy")}";

            dbContext.PayrollCronJobConfigs.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            rsp.Response = mapper.Map<PayrollCronJobConfigViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeletePayrollCronJobConfigCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.PayrollCronJobConfigs.FindAsync(request.Id);

            dbContext.PayrollCronJobConfigs.Remove(entity);
            await dbContext.SaveChangesAsync();

            rsp.Response = "Job successfully deleted";

            return rsp;
        }
    }
}


