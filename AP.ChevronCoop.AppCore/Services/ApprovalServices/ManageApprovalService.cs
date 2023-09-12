using AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using MediatR;
using AP.ChevronCoop.AppCore.Services.AccountAutoCreationServices;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices;

public class ManageApprovalService : BaseApprovalFactory, IManageApprovalService
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly ILoggerService _loggerService;
    private readonly ILoggerFactory loggerFactory;

    private readonly CoreAppSettings _options;
    private readonly IMapper _mapper;
    private readonly IApprovalDetailFactory _approvalDetailFactory;
    private readonly IMediator _mediator;
    private readonly IAutoCreateAccountService _autoCreateAccountService;


    public ManageApprovalService(ChevronCoopDbContext dbContext, ILoggerService loggerService, ILoggerFactory logger,
        IAutoCreateAccountService autoCreateAccountService,
        IOptions<CoreAppSettings> options, IMapper mapper, IMediator mediator, IApprovalDetailFactory approvalDetailFactory)
        : base(dbContext, loggerService, logger, mapper, mediator)
    {
        _dbContext = dbContext;
        _loggerService = loggerService;
        loggerFactory = logger;
        _options = options.Value;
        _mapper = mapper;
        _approvalDetailFactory = approvalDetailFactory;
        _autoCreateAccountService = autoCreateAccountService;
    }
    public async Task<CommandResult<Approval>> CreateApproval(CreateApprovalModel request, bool useDefaultApprovalflow,
        string? approvalWorkflowId = null)
    {
        var rsp = new CommandResult<Approval>();

        try
        {
            if (!useDefaultApprovalflow && approvalWorkflowId is null)
            {
                rsp.Message = "Approval flow is required";
                rsp.StatusCode = StatusCodes.Status400BadRequest;
                return rsp;
            }

            if (useDefaultApprovalflow)
            {
                var approvalWorkflow =
                    await _dbContext.ApprovalWorkflows.FirstOrDefaultAsync(x => x.WorkflowName.ToLower() == "default");

                if (approvalWorkflow == null)
                {
                    rsp.Message = "Default approval not set.";
                    rsp.StatusCode = StatusCodes.Status400BadRequest;
                    return rsp;
                }

                approvalWorkflowId = approvalWorkflow.Id;
            }


            var entity = _mapper.Map<Approval>(request);
            entity.ApprovalWorkflowId = approvalWorkflowId;
            entity.Status = ApprovalStatus.CREATED;
            entity.ApprovalType = request.ApprovalType;
            entity.CreatedByUserId = request.CreatedBy;
            entity.EntityId = request.EntityId;
            entity.CurrentSequence = 0;
            entity.Module = request.Module;

            _dbContext.Approvals.Add(entity);
            await _dbContext.SaveChangesAsync();

            await _approvalDetailFactory.ProcessProviderApprovalDetails(entity);

            rsp.Response = entity;

            rsp.Message = "Approval created successfully.";
            rsp.StatusCode = StatusCodes.Status201Created;
        }
        catch (Exception e)
        {
            rsp.Message = "Approval not created.";
            rsp.StatusCode = StatusCodes.Status400BadRequest;
        }
        return rsp;
    }

    public async Task<CommandResult<string>> HandleApproval(HandleApprovalCommand request)
    {

        var rsp = new CommandResult<string>();
        var approval = await _dbContext.Approvals
            .Include(z => z.ApprovalWorkflow)
            .FirstOrDefaultAsync(x => x.Id == request.ApprovalId);

        if (approval is null)
        {
            rsp.Message = "Approval not found.";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            return rsp;
        }

        if (approval.IsApprovalCompleted)
        {
            rsp.Message = "This request has already been processed";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            return rsp;
        }

        if (approval.ApprovalType == ApprovalType.KYC_COMPLETION)
        {
            if (!await _autoCreateAccountService.CheckDefaultProductsAsync())
            {
                // System also sends a mail notification to both Admin and Internal control: prompting them to create default product.
                rsp.Message = "Default savings and Special Deposit products do not exist. Kindly setup a default products to proceed.";
                rsp.Response = "No Default Product for SD Accounts Creation.";
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                return rsp;
            }
        }

        // Get the approval workflow
        var workflow = approval.ApprovalWorkflow;

        if (workflow is null)
        {
            rsp.Message = "Approval workflow not found.";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            return rsp;
        }

        var approvalWorkflowGroupsQuery = _dbContext.ApprovalGroupWorkflows
            .Where(x => x.ApprovalWorkflowId == workflow.Id);

        // Get total number of approval groups in the workflow
        var totalApprovalGroups = approvalWorkflowGroupsQuery.Count();
        var totalRequiredApprovers = approvalWorkflowGroupsQuery.Sum(x => x.RequiredApprovers);

        // Get the approval workflow groups
        var approvalGroupFlow = _dbContext.ApprovalGroupWorkflows
            .Where(x => x.ApprovalWorkflowId == workflow.Id)
            .OrderBy(o => o.Sequence)
            .Skip(approval.CurrentSequence)
            .FirstOrDefault();

        if (approvalGroupFlow == null)
        {
            rsp.Message = "You can't approve this request.";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            return rsp;
        }

        // Get the approval group member
        var approvalGroupMember = _dbContext.ApprovalGroupMembers
            .Include(g => g.ApprovalGroup)
            .Where(x => x.ApprovalGroupId == approvalGroupFlow.ApprovalGroupId)
            .FirstOrDefault(x => x.ApplicationUserId == request.ApplicationUserId);

        // Check if the approval group members contains the user
        if (approvalGroupMember == null)
        {
            rsp.Message = "You can't approve this request.";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            return rsp;
        }

        // Get logged approvals
        var totalApprovalLogQuery = _dbContext.ApprovalLogs.Where(x => x.ApprovalId == approval.Id);
        var groupLoggedApprovals = totalApprovalLogQuery.Where(x => x.ApprovalGroupId == approvalGroupMember.ApprovalGroupId);

        if (totalApprovalLogQuery.Any(x => x.ApprovalGroupId == approvalGroupMember.ApprovalGroupId && x.ApprovedByUserId == request.ApplicationUserId))
        {
            rsp.Message = "You've submitted an approval for this request.";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            return rsp;
        }

        if (groupLoggedApprovals.Count() >= approvalGroupFlow.RequiredApprovers)
        {
            rsp.Message = "Request already approved by order group members";
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            return rsp;
        }

        approval.DateUpdated = DateTimeOffset.Now;
        approval.UpdatedByUserId = request.ApplicationUserId;
        approval.Comment = request.Comment;

        // Close approval once anyone reject
        if (request.Status == ApprovalStatus.REJECTED)
        {
            approval.Status = request.Status;
            approval.IsApprovalCompleted = true;

            await ExecuteApproval(approval, request);
        }


        if (request.Status == ApprovalStatus.APPROVED)
        {
            // Check if the user is the last approver in the workflow
            if (totalRequiredApprovers == totalApprovalLogQuery.Count() + 1)
            {
                approval.IsApprovalCompleted = true;
                approval.Status = request.Status;
                approval.DateUpdated = DateTimeOffset.Now;
                approval.UpdatedByUserId = request.ApplicationUserId;
                approval.Comment = request.Comment;

                await ExecuteApproval(approval, request);
            }
            else
            {
                // approval.Status = ApprovalStatus.ON_GOING;

                // Check if the user is the last approver in the group
                if (approvalGroupFlow.RequiredApprovers == groupLoggedApprovals.Count() + 1)
                {
                    approval.CurrentSequence += 1;

                    try
                    {
                        await _mediator.Send(new SendApprovalRequestNotificationCommand
                        {
                            ApprovalId = approval.Id,
                            Sequence = approval.CurrentSequence
                        });
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
            }
        }

        // Create Approval log
        var approvalLog = new ApprovalLog
        {
            ApprovalId = approval.Id,
            Comment = request.Comment,
            CreatedByUserId = request.ApplicationUserId,
            DateCreated = DateTimeOffset.Now,
            Status = request.Status,
            Sequence = approval.CurrentSequence,
            ApprovalGroupId = approvalGroupMember.ApprovalGroupId,
            ApprovedByUserId = request.ApplicationUserId
        };

        _dbContext.ApprovalLogs.Add(approvalLog);
        _dbContext.Approvals.Update(approval);
        await _dbContext.SaveChangesAsync();

        if (approval.ApprovalType == ApprovalType.KYC_COMPLETION)
        {
            var result = await _autoCreateAccountService.GetCreateSpecialAndSavingsAccountResultAsync(request.ApplicationUserId);

            if (result.Response.Completed)
            {
                rsp.Message = result.Response.Message;
                rsp.Response = "Successful Operation.";
                rsp.StatusCode = (int)HttpStatusCode.OK;
                return rsp;
            }
        }

        rsp.Message = "You have processed your approval successfully.";
        rsp.Response = "Successful Operation.";
        rsp.StatusCode = (int)HttpStatusCode.OK;
        return rsp;
    }

    private async Task<CommandResult<bool>> ExecuteApproval(Approval approval, HandleApprovalCommand request)
    {
        // Update approval
        var provider = GetProvider(approval.ApprovalType);
        return await provider.Process(approval, request.ApplicationUserId, request.Comment, request.Status);
    }

}



