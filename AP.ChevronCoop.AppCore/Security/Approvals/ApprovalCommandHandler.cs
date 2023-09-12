using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Security.Approvals;

public class ApprovalCommandHandler :
    IRequestHandler<QueryApprovalCommand, CommandResult<IQueryable<Approval>>>,
    // IRequestHandler<QueryUserApprovalCommand, CommandResult<IQueryable<ApprovalView>>>,
    IRequestHandler<QueryUserApprovalCommand, CommandResult<IQueryable<ApprovalView>>>,
    IRequestHandler<CreateApprovalCommand, CommandResult<ApprovalViewModel>>,
    IRequestHandler<UpdateApprovalCommand, CommandResult<ApprovalViewModel>>,
    IRequestHandler<DeleteApprovalCommand, CommandResult<string>>,
    IRequestHandler<HandleApprovalCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly ILogger logger;
    private readonly IMapper _mapper;
    private readonly CoreAppSettings _options;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IEmailService _emailService;
    private readonly IManageApprovalService _manageApprovalService;

    public ApprovalCommandHandler(ChevronCoopDbContext appDbContext, IOptions<CoreAppSettings> options,
        UserManager<ApplicationUser> userManager, IManageApprovalService manageApprovalService,
        ILogger<ApprovalCommandHandler> _logger, IMapper mapper, RoleManager<ApplicationRole> roleManager,
        IEmailService emailService)
    {
        _dbContext = appDbContext;
        logger = _logger;
        _mapper = mapper;
        _options = options.Value;
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
        _manageApprovalService = manageApprovalService;
    }

    public Task<CommandResult<IQueryable<Approval>>> Handle(QueryApprovalCommand request,CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<Approval>>();
        rsp.Response = _dbContext.Approvals;
        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<ApprovalViewModel>> Handle(CreateApprovalCommand request,CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<ApprovalViewModel>();

        try
        {
            var entity = _mapper.Map<Approval>(request);

            await _dbContext.Approvals.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = _mapper.Map<ApprovalViewModel>(entity);
            rsp.Message = "Approval created successful.";
            return rsp;
        }
        catch (Exception exp)
        {
            rsp.ErrorFlag = true;
            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
            rsp.Message = exp.Message;

            return rsp;
        }
    }

    public async Task<CommandResult<ApprovalViewModel>> Handle(UpdateApprovalCommand request,
        CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<ApprovalViewModel>();
        var approvalEntity = await _dbContext.Approvals.FindAsync(request.Id);
        if (approvalEntity == null)
        {
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Message = "Approval could not be updated.";
            return rsp;
        }

        var approvalWorkflow = await _dbContext.ApprovalWorkflows.FindAsync(request.ApprovalWorkflowId);
        if (approvalWorkflow == null)
        {
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Message = "ApprovalWorkflow was not found.";
            return rsp;
        }

        //approvalEntity = CheckAndUpdateApprovalFlow(approvalEntity, approvalWorkflow, request);
        _dbContext.Approvals.Update(approvalEntity);
        await _dbContext.SaveChangesAsync();
        rsp.Response = _mapper.Map<ApprovalViewModel>(approvalEntity);

        request.EmailTitle = "Approval Creation";
        var emailBody = $@"
                <html>
                    <body>
                        <p>A request has been sent for your approval</p>
                        <p>Kindly follow the link bellow to process your approval. </p>
                        <p>Approval link: <a href='{_options.WebBaseUrl}'> {_options.WebBaseUrl} </a></p>
                    </body>
                </html>
            ";

        //await ScheduleEmailAlertForApproval(approvalEntity, approvalWorkflow.SequentialApprovalJson, request.EmailTitle, emailBody);
        rsp.StatusCode = (int)HttpStatusCode.OK;
        rsp.Response = _mapper.Map<ApprovalViewModel>(approvalEntity);
        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteApprovalCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var entity = await _dbContext.Approvals.FindAsync(request.Id);
        entity.IsDeleted = true;
        _dbContext.Approvals.Update(entity!);
        await _dbContext.SaveChangesAsync(cancellationToken);
        rsp.Response = "Data successfully deleted";
        return rsp;
    }

    public async Task<CommandResult<string>> Handle(HandleApprovalCommand request, CancellationToken cancellationToken)
    {
        return await _manageApprovalService.HandleApproval(request);
    }


    //public async Task<CommandResult<IQueryable<ApprovalMasterView>>> Handle(QueryUserApprovalCommand request, CancellationToken cancellationToken)
    //{
    //    var rsp = new CommandResult<IQueryable<ApprovalMasterView>>();
        
    //    // Get user groups
    //    var userGroups = _dbContext.ApprovalGroupMembers
    //        .Where(x => x.ApplicationUserId == request.ApplicationUserId)
    //        .Select(c => c.ApprovalGroupId);

    //    var approvalWorkflows = _dbContext.ApprovalGroupWorkflows
    //        .Where(g => userGroups.Contains(g.ApprovalGroupId))
    //        .Select(g => g.ApprovalWorkflowId).ToList();

    //    var userApprovals = _dbContext.ApprovalMasterView.Where(z => approvalWorkflows.Contains(z.ApprovalWorkflowId));

    //    rsp.Response = userApprovals;
    //    return rsp;
    //}

    public async Task<CommandResult<IQueryable<ApprovalView>>> Handle(QueryUserApprovalCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<ApprovalView>>();

        // Get user groups
        var userGroups = _dbContext.ApprovalGroupMembers
            .Where(x => x.ApplicationUserId == request.ApplicationUserId)
            .Select(c => c.ApprovalGroupId);
        
        var approvalWorkflows = _dbContext.ApprovalGroupWorkflows
            .Where(g => userGroups.Contains(g.ApprovalGroupId))
            .Select(g => g.ApprovalWorkflowId).ToList();
        
        var userApprovals = _dbContext.ApprovalView
            .Where(z => approvalWorkflows.Contains(z.ApprovalWorkflowId));
        rsp.Response = userApprovals;
        
        return rsp;
    }
}

