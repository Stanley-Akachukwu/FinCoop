using AP.ChevronCoop.AppCore.Loans.LoanTopups;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class BaseApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILoggerFactory loggerFactory;
    private readonly ILoggerService loggerService;
    public BaseApprovalFactory(ChevronCoopDbContext dbContext, ILoggerService _loggerService, ILoggerFactory _logger,
        IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
        loggerService = _loggerService;
        loggerFactory = _logger;
    }

    public IApprovalFactory GetProvider(ApprovalType type)
    {
        switch (type)
        {
            case ApprovalType.LOAN_PRODUCT:
                return new LoanProductApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.DEPOSIT_PRODUCT:
                return new DepositProductApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.RETIREE_SWITCH:
                return new RetireeSwitchApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.KYC_COMPLETION:
                return new MemberKYCApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.MEMBER_BULK_UPLOAD:
                return new BulkUploadApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.WORKFLOW_SETUP:
                return new WorkflowSetupApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SAVING_DEPOSIT_APPLICATION:
                return new SavingDepositApplicationApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.FIXED_DEPOSIT_APPLICATION:
                return new FixedDepositApplicationApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.FIXED_DEPOSIT_CHANGE_IN_MATURITY:
                return new FixedDepositChangeInMaturityApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.FIXED_DEPOSIT_LIQUIDATION:
                return new FixedDepositImediateLiquidationApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.LOAN_PRODUCT_APPLICATION:
                return new LoanApplicationApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SPECIAL_DEPOSIT_APPLICATION:
                return new SpecialDepositApplicationApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SAVINGS_INCREASE_DECREASE:
                return new SavingIncreaseDecreaseApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SAVINGS_CASH_ADDITION:
                return new SavingCashAdditionApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SPECIAL_DEPOSIT_CASH_ADDITION:
                return new SpecialDepositCashAdditionApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SPECIAL_DEPOSIT_FUND_TRANSFER:
                return new SpecialDepositFundTransferApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SPECIAL_DEPOSIT_WITHDRAWAL:
                return new SpecialDepositWithdrawalApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.SPECIAL_DEPOSIT_INCREASE_DECREASE:
                return new SpecialIncreaseDecreaseApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.LOAN_DISBURSEMENT:
                return new LoanDisbursementApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.LOAN_DISBURSEMENT_TOPUP:
                return new LoanDisbursementTopupApprovalFactory(_dbContext, _mapper, _mediator);
            case ApprovalType.LOAN_TOPUP_APPLICATION:
                return new LoanTopupApprovalFactory(_dbContext, loggerService, loggerFactory, _mapper, _mediator);
            case ApprovalType.LOAN_OFFSET_APPLICATION:
                return new LoanOffsetApprovalFactory(_dbContext, _mapper, _mediator);
            default:
                return new DefaultApprovalFactory(_dbContext, _mapper, _mediator);
        }
    }


}