using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class SpecialDepositCashAdditionApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SpecialDepositCashAdditionApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator )
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

            var entity = _dbContext.SpecialDepositCashAdditions.FirstOrDefault(x => x.Id == request.EntityId);

            if (entity != null)
            {
                var command = new UpdateSpecialDepositCashAdditionCommand
                {
                    Id = request.EntityId,
                    SpecialDepositAccountId = entity.SpecialDepositAccountId,
                    Amount = entity.Amount,
                    ModeOfPayment = entity.ModeOfPayment,
                    CustomerPaymentDocumentId = entity.CustomerPaymentDocumentId,
                    CreatedByUserId = approvedById,
                };

                await _mediator.Send(command);
                response.Response = true;

            }


        }
        catch (Exception ex)
        {
            response.Response = false;
            response.Message = ex.Message;
        }
        return response;
    }
}

