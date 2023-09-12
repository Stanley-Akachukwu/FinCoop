using AP.ChevronCoop.AppCore.ChevronAPIs.Interfaces;
using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.Loans.LoanApplicationItems;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Infrastructure.Services.ChevronAPIs.Dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplications;

public class LoanApplicationCommandHandler :
    IRequestHandler<QueryLoanApplicationCommand, CommandResult<IQueryable<LoanApplication>>>,
    IRequestHandler<GenerateScheduleCommand, CommandResult<GenerateScheduleViewModel>>,
    IRequestHandler<LoanApplicationEligibilityCommand, CommandResult<LoanApplicationEligibilityViewModel>>,
    IRequestHandler<CreateLoanApplicationCommand, CommandResult<LoanApplicationViewModel>>
{
    private readonly IManageApprovalService _approval;
    private readonly IEmailService _emailService;
    private readonly IMediator _mediator;
    private readonly IChevronNetPayService _netPayService;
    private readonly CoreAppSettings _options;
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger logger;
    private readonly IMapper mapper;

    public LoanApplicationCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<LoanApplicationCommandHandler> _logger, IMapper _mapper, IManageApprovalService approval,
        IOptions<CoreAppSettings> options, IEmailService emailService, IMediator mediator, IChevronNetPayService netPayService)
    {
        dbContext = appDbContext;
        logger = _logger;
        mapper = _mapper;
        _approval = approval;
        _emailService = emailService;
        _options = options.Value;
        _mediator = mediator;
        _netPayService = netPayService;
    }


    public async Task<CommandResult<LoanApplicationViewModel>> Handle(CreateLoanApplicationCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanApplicationViewModel>();
        var loanProduct = await dbContext.LoanProducts.Where(x => x.Id == request.LoanProductId)
            .FirstOrDefaultAsync(cancellationToken);
       
        var entity = mapper.Map<LoanApplication>(request);
        entity.ApplicationNo = NHiloHelper.GetNextKey(nameof(LoanApplication)).ToString();
        entity.Principal = request.Amount;
        entity.Status = LoanApplicationStatus.PENDING;
        entity.SpecialDepositId = request.SpecialDepositId;
        entity.CustomerDisbursementAccountId = request.DestinationAccountId;
        entity.UseSpecialDeposit = request.UseSpecialDeposit;
        dbContext.LoanApplications.Add(entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        // Capture Guarantors
        if (request.Guarantors != null && request.Guarantors.Any())
        {
            var guarantorIds = request.Guarantors.Select(x => x.GuarantorCustomerId);

            var guarantorProfiles = await dbContext.Customers.Where(x => guarantorIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var loanGuarantors = new List<LoanApplicationGuarantor>();

            foreach (var profile in guarantorProfiles)
            {
                // get guarantor type
                var guarantorType = request.Guarantors.FirstOrDefault(x => x.GuarantorCustomerId == profile.Id)
                ?.GuarantorType;

                if (guarantorType != null)
                    loanGuarantors.Add(new LoanApplicationGuarantor
                    {
                        LoanApplicationId = entity.Id,
                        GuarantorType = (GuarantorType)guarantorType,
                        GuarantorId = profile.Id,
                        Status = ApprovalStatus.PENDING_APPROVAL,
                        GuarantorApprovalType = GuarantorApprovalType.LOAN_APPLICATION
                    }
                    );

                // TODO: Send email to guarantors
                var props = new GeneralEmailDto
                {
                    Link = $"{_options.WebBaseUrl}",
                    Name = $"{profile.FirstName} {profile.LastName}"
                };

                _ = _emailService.SendEmailAsync(EmailTemplateType.GUARANTOR_APPROVAL, profile.PrimaryEmail, props);
            }

            await dbContext.LoanApplicationGuarantors.AddRangeAsync(loanGuarantors, cancellationToken);

            entity.Status = LoanApplicationStatus.AWAITING_GUARANTOR_APPROVAL;
            dbContext.LoanApplications.Update(entity);
        }


        // Capture Application Items
        if (request.Items != null && request.Items.Any())
        {
            var applicationItems = new List<LoanApplicationItem>();
            foreach (var applianceDetails in request.Items)
                applicationItems.Add(new LoanApplicationItem
                {
                    LoanApplicationId = entity.Id,
                    Amount = applianceDetails.Amount,
                    Color = applianceDetails.Color,
                    Model = applianceDetails.Model,
                    Name = applianceDetails.Name,
                    BrandName = applianceDetails.BrandName,
                    ItemType = applianceDetails.ItemType
                });

            await dbContext.LoanApplicationItems.AddRangeAsync(applicationItems, cancellationToken);
        }

        // Capture application schedules
        var command = new CreateLoanApplicationScheduleCommand
        {
            Amount = request.Amount
        };

        if (loanProduct != null) command.Interest = loanProduct.InterestRate;
        command.CommencementDate = request.RepaymentCommencementDate;
        command.TenureUnit = request.TenureUnit;
        command.TenureValue = request.TenureValue;

        var applicationScheduleResult = await _mediator.Send(command, cancellationToken);
        if (applicationScheduleResult.Response is not null)
        {
            var applicationSchedule = mapper.Map<List<LoanApplicationSchedule>>(applicationScheduleResult.Response);
            applicationSchedule.ForEach(x =>
                x.RepaymentNo = (int)NHiloHelper.GetNextKey(nameof(LoanApplicationSchedule)));
            
            applicationSchedule.ForEach(x => x.LoanApplicationId = entity.Id);
            await dbContext.LoanApplicationSchedules.AddRangeAsync(applicationSchedule, cancellationToken);
        }

        if (request.Guarantors == null || !request.Guarantors.Any())
        {
            var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken: cancellationToken);

            
            var approvalRequest = new CreateApprovalModel
            {
                Module = "LoanProductApplication",
                Payload = JsonConvert.SerializeObject(request),
                Comment = "Create loan product approval initiated",
                ApprovalType = ApprovalType.LOAN_PRODUCT_APPLICATION,
                Description =
                    $"Loan Application - {customer?.FirstName} {customer?.MiddleName} {customer?.LastName} ({entity.ApplicationNo})",
                CreatedBy = request.ApplicationUserId,
                EntityId = entity.Id,
                EntityType = typeof(CreateLoanApplicationCommand)
            };

            var approval = await _approval.CreateApproval(approvalRequest, false, loanProduct?.ApprovalWorkflowId);

            if (approval.StatusCode == StatusCodes.Status201Created)
            {
                entity.ApprovalId = approval.Response.Id;
                dbContext.LoanApplications.Update(entity);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        rsp.Response = mapper.Map<LoanApplicationViewModel>(entity);

        return rsp;
    }

    public Task<CommandResult<IQueryable<LoanApplication>>> Handle(QueryLoanApplicationCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<LoanApplication>>();
        rsp.Response = dbContext.LoanApplications;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<GenerateScheduleViewModel>> Handle(GenerateScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<GenerateScheduleViewModel>();
        var customer = await dbContext.Customers.Where(x => x.Id == request.CustomerId)
            .FirstOrDefaultAsync(cancellationToken);

        //var schedule = InterestCalculatorHelper.SimpleInterestLoanComputation(request.TenureUnit, request.TenureValue,
        //    request.CommencementDate, request.Principal, request.InterestRate);

        var loan = new LoanHelper(request.Principal, request.InterestRate, request.InterestMethod, request.InterestCalculationMethod,
        request.TenureUnit, request.TenureValue, request.RepaymentPeriod, request.DaysInYear, request.CommencementDate);


        List<AmortizationSchedule> schedule = loan.GetAmortizationTable(loan.InterestCalculationMethod);

        var verify = new EmployeeCollectLoanRequestDto()
        {
            Year = request.CommencementDate.Year,
            EmployeeNo = customer!.CAI,
            MonthlyRepayment = (long)request.Principal,
            CurrentMonthlyExposure = request.CommencementDate.Month,
            RepayStartMonth = request.CommencementDate.Month,
            Voucher = "Y"
        };

        var response = new GenerateScheduleViewModel
        {
            Schedules = schedule,
            IsApproved = true //await _netPayService.CanEmployeeCollectLoanAsync(verify)
        };

        rsp.Response = response;

        return rsp;
    }

    public async Task<CommandResult<LoanApplicationEligibilityViewModel>> Handle(LoanApplicationEligibilityCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<LoanApplicationEligibilityViewModel>();
        var product = await dbContext.LoanProducts.FirstOrDefaultAsync(x => x.Id == request.LoanProductId, cancellationToken: cancellationToken);

        bool _isEligible = true;
        string _reason = string.Empty;

        var expectedAmount = (product.QualificationMinBalancePercentage / 100) * request.Amount;
        switch (product.QualificationTargetProduct)
        {
            case DepositProductType.SAVINGS:
            {
                var customerSavings = await dbContext.SavingsAccounts
                    .Include(y => y.LedgerDepositAccount)
                    .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId, cancellationToken: cancellationToken);

                if (customerSavings != null)
                {
                    _isEligible = customerSavings.LedgerDepositAccount.LedgerBalance >= expectedAmount;
                    _reason = _isEligible ? "Customer is eligible for the product": "Customer savings balance is lower than minimum balance";
                }
                else
                {
                    _isEligible = false;
                    _reason = "Customer savings account not found";
                }
                
                break;
            }
            
            case DepositProductType.FIXED_DEPOSIT:
            {
                var customerSavings = await dbContext.FixedDepositAccounts
                    .Include(y => y.DepositAccount)
                    .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId, cancellationToken: cancellationToken);

                if (customerSavings != null)
                {
                    _isEligible = customerSavings.DepositAccount.LedgerBalance >= expectedAmount;
                    _reason = _isEligible ? "Customer is eligible for the product": "Customer fixed deposit balance is lower than minimum balance";
                }
                else
                {
                    _isEligible = false;
                    _reason = "Customer fixed deposit account not found";
                }
                
                break;
            }
            
            case DepositProductType.SPECIAL_DEPOSIT:
            {
                var customerSavings = await dbContext.SpecialDepositAccounts
                    .Include(y => y.DepositAccount)
                    .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId, cancellationToken: cancellationToken);

                if (customerSavings != null)
                {
                    _isEligible = customerSavings.DepositAccount.LedgerBalance >= expectedAmount;
                    _reason = _isEligible ? "Customer is eligible for the product": "Customer special deposit balance is lower than minimum balance";
                }
                else
                {
                    _isEligible = false;
                    _reason = "Customer special deposit account not found";
                }
                
                break;
            }
        }
        
        var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken: cancellationToken);
        if (customer!.DateOfEmployment != null)
        {
            var dateDiff = DateTime.Now - customer.DateOfEmployment;
            var months = dateDiff.Value.Days / (365.25 / 12);
            
            _isEligible = months > 6;
            _reason = _isEligible ? "Customer is eligible for the product" : "Customer has not been employed for up to 6 months";
        }

        var response = new LoanApplicationEligibilityViewModel
        {
            IsEligible = _isEligible,
            Reason = _reason
        };

        rsp.Response = response;
        return rsp;
    }
}