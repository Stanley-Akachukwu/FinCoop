using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory
{
    public class WorkflowSetupApprovalFactory : IApprovalFactory
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public WorkflowSetupApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
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

                var entity = await _dbContext.ApprovalWorkflows.FirstOrDefaultAsync(x => x.Id == request.EntityId);
                if (entity == null)
                {
                    response.Response = false;
                    response.Message = "Workflow Setup not found";
                    return response;
                }

                _dbContext.ApprovalWorkflows.Update(entity);

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
}
