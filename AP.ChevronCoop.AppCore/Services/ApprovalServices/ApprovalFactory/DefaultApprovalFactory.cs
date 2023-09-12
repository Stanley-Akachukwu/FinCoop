using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class DefaultApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public DefaultApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public async Task<CommandResult<bool>> Process(Approval request, string? approvedById, string? comment, ApprovalStatus status)
    {
        var response = new CommandResult<bool>();

        response.Response = await _mediator.Send(new SendApprovalRequestNotificationCommand()
        {
            ApprovalId = request.Id
        });

        return response;
    }

    public async Task<CommandResult<bool>> Initiate(Approval request)
    {
        var response = new CommandResult<bool>();
        response.Response = true;

        return response;
    }
}