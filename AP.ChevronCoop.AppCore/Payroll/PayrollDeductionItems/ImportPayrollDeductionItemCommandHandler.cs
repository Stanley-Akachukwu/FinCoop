using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Payroll;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollCronJobConfigs
{

    public class ImportPayrollDeductionItemCommandHandler :

        IRequestHandler<ImportPayrollDeductionItemCommand, CommandResult<string>>
    {
        private readonly ILoggerService _logger;
        private readonly ChevronCoopDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IManageApprovalService _approvalLog;

        public ImportPayrollDeductionItemCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator,
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

        public async Task<CommandResult<string>> Handle(ImportPayrollDeductionItemCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();

             var payrollDeductionItems =   mapper.Map<List<PayrollDeductionItem>>(request.PayrollDeductionItems);
            for (int i = 0; i < payrollDeductionItems.Count; i++)
                payrollDeductionItems[i].CreatedByUserId = request.CreatedByUserId;


            dbContext.PayrollDeductionItems.AddRange(payrollDeductionItems);
            dbContext.SaveChanges();
            rsp.Message = "Successfull.";
            rsp.Response = "Payroll schedule items imported successfully.";
            return rsp;
        }

        
    }
}


