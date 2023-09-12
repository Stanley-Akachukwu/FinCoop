using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class FixedDepositApplicationApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public FixedDepositApplicationApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CommandResult<bool>> Initiate(Approval request)
    {
        var response = new CommandResult<bool>();

        response.Response = await _mediator.Send(new SendApprovalRequestNotificationCommand()
        {
            ApprovalId = request.Id
        });

        return response;
    }

    public async Task<CommandResult<bool>> Process(Approval request, string? approvedById, string? comment, ApprovalStatus status)
    {
        var response = new CommandResult<bool>();
        try
        {
            if (status != ApprovalStatus.APPROVED)
            {
                response.Response = false;
                response.Message = "Request Not Approved.";
                return response;
            }

            var application = _dbContext.FixedDepositAccountApplications.FirstOrDefault(x => x.Id == request.EntityId);

            if (application != null)
            {
                var command = new CreateFixedDepositAccountCommand
                {
                    ApplicationId = application.Id,
                    Amount = application.Amount,
                    CreatedByUserId = application.CreatedByUserId,
                    CustomerId = application.CustomerId,
                    DepositProductId = application.DepositProductId,
                    LiquidationAccountType = application.LiquidationAccountType,
                    MaturityInstructionType = application.MaturityInstructionType,
                    InterestRate = application.InterestRate,
                    TenureUnit = application.TenureUnit,
                    TenureValue = application.TenureValue
                };


                if (command.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT) command.LiquidationAccountId = application.SavingsLiquidationAccountId;

                if (command.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT) command.LiquidationAccountId = application.SpecialDepositLiquidationAccountId;

                if (command.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT) command.LiquidationAccountId = application.CustomerBankLiquidationAccountId;


                await _mediator.Send(command);

            }


            response.Response = true;
        }
        catch (Exception e)
        {
            response.Response = false;
            response.Message = e.Message;
        }

        return response;
    }
}