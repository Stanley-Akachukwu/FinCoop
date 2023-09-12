using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class FixedDepositChangeInMaturityApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    public FixedDepositChangeInMaturityApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
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

            var model = _dbContext.FixedDepositChangeInMaturities
                   .FirstOrDefault(x => x.Id == request.EntityId);
            
            if (model != null)
            {
                var payload = System.Text.Json.JsonSerializer.Deserialize<CreateFixedDepositChangeInMaturityCommand>(request.Payload);

                var command = new UpdateFixedDepositChangeInMaturityCommand
                {
                    Id = model.Id,
                    FixedDepositAccountId = model.FixedDepositAccountId,
                    MaturityInstructionType = model.MaturityInstructionType,
                    LiquidationAccountId = payload.LiquidationAccountId,
                    LiquidationAccountType = payload.LiquidationAccountType

                };
                await _mediator.Send(command);
                response.Response = true;

            }

            
        }
        catch (Exception e)
        {
            response.Response = false;
            response.Message = e.Message;
        }

        return response;
    }
}