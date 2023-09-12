using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupWorkflows;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalWorkflows
{
    public class ApprovalWorkflowCommandHandler :
     IRequestHandler<QueryApprovalWorkflowCommand, CommandResult<IQueryable<ApprovalWorkflow>>>,
    IRequestHandler<CreateApprovalWorkflowCommand, CommandResult<ApprovalWorkflowViewModel>>,
    IRequestHandler<UpdateApprovalWorkflowCommand, CommandResult<ApprovalWorkflowViewModel>>,
    IRequestHandler<DeleteApprovalWorkflowCommand, CommandResult<string>>,
    IRequestHandler<GetApprovalWorkflowByIdCommand, CommandResult<GetApprovalWorkflowViewModel>>


    {

        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IManageApprovalService _approval;
        public ApprovalWorkflowCommandHandler(ChevronCoopDbContext appDbContext,
        ILogger<ApprovalCommandHandler> logger, IMapper mapper, IManageApprovalService approval)
        {

            _dbContext = appDbContext;
            _logger = logger;
            _mapper = mapper;
            _approval = approval;
        }
        public async Task<CommandResult<IQueryable<ApprovalWorkflow>>> Handle(QueryApprovalWorkflowCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<ApprovalWorkflow>>();
            rsp.Response = _dbContext.ApprovalWorkflows;
            return await Task.FromResult(rsp);
        }

        public async Task<CommandResult<ApprovalWorkflowViewModel>> Handle(CreateApprovalWorkflowCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ApprovalWorkflowViewModel>();
            var entity = new ApprovalWorkflow
            {
                WorkflowName = request.WorkflowName,
                CreatedByUserId = request.CreatedByUserId
            };

            entity.Description = $"{request.WorkflowName} setup for product approvals.";
            entity.RequiredApprovers = request.ApprovalGroups.Sum(x => x.RequiredApprovers);
            entity.RequiredGroups = request.ApprovalGroups.Count;
            _dbContext.ApprovalWorkflows.Add(entity);

            var groupIds = request.ApprovalGroups.Select(x => x.GroupId);

            var approvalGroups = await _dbContext.ApprovalGroups.Where(x => groupIds.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);
            var groupWorkflows = new List<ApprovalGroupWorkflow>();

            var groups = request.ApprovalGroups.OrderBy(x => x.ApprovalSequence);
            foreach (var approvalGroup in groups)
            {
                var group = approvalGroups.FirstOrDefault(x => x.Id == approvalGroup.GroupId);
                if (group != null)
                {
                    groupWorkflows.Add(new ApprovalGroupWorkflow()
                    {
                        ApprovalGroupId = group.Id,
                        ApprovalWorkflowId = entity.Id,
                        RequiredApprovers = approvalGroup.RequiredApprovers,
                        Sequence = approvalGroup.ApprovalSequence
                    });
                }
            }

            await _dbContext.ApprovalGroupWorkflows.AddRangeAsync(groupWorkflows, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var approvalGroupWorkflow = await _dbContext.ApprovalGroupWorkflows
                .Include(x => x.ApprovalGroup)
                .Where(x => x.ApprovalWorkflowId == entity!.Id)
                .AsNoTracking()
                .ToListAsync();

            entity!.ApprovalGroups = approvalGroupWorkflow.Select(x => x.ApprovalGroup).ToList();

            rsp.Response = _mapper.Map<ApprovalWorkflowViewModel>(entity);
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Message = "Approval Workflow created successfully.";
            return rsp;
        }

        public async Task<CommandResult<GetApprovalWorkflowViewModel>> Handle(GetApprovalWorkflowByIdCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<GetApprovalWorkflowViewModel>();
            var response = new GetApprovalWorkflowViewModel();
            var approvalWorkflow = await _dbContext.ApprovalWorkflows.FindAsync(request.Id);

            response.WorkflowName = approvalWorkflow!.WorkflowName;
            response.Id = approvalWorkflow.Id;
            
            response.ApprovalGroups  = await _dbContext.ApprovalGroupWorkflows
                .Include(x => x.ApprovalGroup)
                .Where(x => x.ApprovalWorkflowId == approvalWorkflow.Id)
                .Select(x => new WorkflowApprovalGroupModel()
                {
                    ApprovalSequence = x.Sequence,
                    GroupId = x.ApprovalGroup.Id,
                    GroupName = x.ApprovalGroup.Name,
                    RequiredApprovers = x.RequiredApprovers
                }).ToListAsync(cancellationToken: cancellationToken);;

            rsp.Response = response;
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Message = "Approval Workflow found.";
            return rsp;
        }
        
        public async Task<CommandResult<ApprovalWorkflowViewModel>> Handle(UpdateApprovalWorkflowCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ApprovalWorkflowViewModel>();

            var entity = await _dbContext.ApprovalWorkflows.FindAsync(request.Id);
            entity!.WorkflowName = request.WorkflowName;
            entity.UpdatedByUserId = request.UpdatedByUserId;
            
            entity.Description = $"{request.WorkflowName} setup for product approvals.";
            entity.RequiredApprovers = request.ApprovalGroups.Sum(x => x.RequiredApprovers);
            entity.RequiredGroups = request.ApprovalGroups.Count;
            _dbContext.ApprovalWorkflows.Update(entity);
            
            var groupIds = request.ApprovalGroups.Select(g => g.GroupId);
            var requestGroupIds = _dbContext.ApprovalGroups
                .Where(x => groupIds.Contains(x.Id))
                .ToHashSet();
        
            var existingGroups = _dbContext.ApprovalGroupWorkflows
                .Include(c => c.ApprovalGroup)
                .Where(x => x.ApprovalWorkflowId == request.Id)
                .Select(v => v.ApprovalGroup)
                .ToHashSet();
        
            // Get groups to be deleted and set isDeleted to true for them
            var groupsToRemove = existingGroups.Except(requestGroupIds).Select(x => x.Id).ToList();
            _dbContext.ApprovalGroupWorkflows.Where(x => groupsToRemove.Contains(x.ApprovalGroupId))
                .ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                    x.DeletedByUserId = request.UpdatedByUserId;
                    x.DateDeleted = DateTime.Now;
                });

            // Add new groups
            var groupWorkflows = new List<ApprovalGroupWorkflow>();
            var newApprovalGroups = requestGroupIds.Except(existingGroups);
            foreach (var approvalGroup in newApprovalGroups)
            {
                var group = request.ApprovalGroups.FirstOrDefault(x => x.GroupId == approvalGroup.Id);
                if (group != null)
                {
                    groupWorkflows.Add(new ApprovalGroupWorkflow()
                    {
                        ApprovalGroupId = approvalGroup.Id,
                        ApprovalWorkflowId = entity.Id,
                        RequiredApprovers = group.RequiredApprovers,
                        Sequence = group.ApprovalSequence,
                        DateUpdated = DateTime.Now,
                        UpdatedByUserId = request.UpdatedByUserId
                    });
                }
            }
            _dbContext.ApprovalGroupWorkflows.AddRange(groupWorkflows);

            await _dbContext.SaveChangesAsync(cancellationToken);

            entity.ApprovalGroups = await _dbContext.ApprovalGroupWorkflows
                .Include(x => x.ApprovalGroup)
                .Where(x => x.ApprovalWorkflowId == entity.Id)
                .Select(x => x.ApprovalGroup)
                .ToListAsync(cancellationToken: cancellationToken);

            rsp.Response = _mapper.Map<ApprovalWorkflowViewModel>(entity);
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Message = "Approval Workflow updated successfully.";

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApprovalWorkflowCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await _dbContext.ApprovalWorkflows.FindAsync(request.Id);

            _dbContext.ApprovalWorkflows.Remove(entity!);
            await _dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }
}
