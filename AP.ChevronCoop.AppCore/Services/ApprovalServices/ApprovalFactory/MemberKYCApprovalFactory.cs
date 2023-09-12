using AP.ChevronCoop.AppCore.Services.AccountAutoCreationServices;
using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class MemberKYCApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public MemberKYCApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
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
                return response;
            }

            var command = new CreateCustomerCommand
            {
                ProfileId = request.EntityId
            };

            var result = await _mediator.Send(command);

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