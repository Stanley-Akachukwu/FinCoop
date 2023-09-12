using System.Net;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroups;

public class ApprovalGroupCommandHandler :
  IRequestHandler<QueryApprovalGroupCommand, CommandResult<IQueryable<ApprovalGroup>>>,
  IRequestHandler<GetApprovalGroupCommand, CommandResult<GetApprovalGroupViewModel>>,
  IRequestHandler<CreateApprovalGroupCommand, CommandResult<ApprovalGroupViewModel>>,
  IRequestHandler<UpdateApprovalGroupCommand, CommandResult<ApprovalGroupViewModel>>,
  IRequestHandler<DeleteApprovalGroupMemberCommand, CommandResult<string>>
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;

    public ApprovalGroupCommandHandler(ChevronCoopDbContext appDbContext, IMapper mapper)
    {
        _dbContext = appDbContext;
        _mapper = mapper;
    }


    public async Task<CommandResult<ApprovalGroupViewModel>> Handle(CreateApprovalGroupCommand request,CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<ApprovalGroupViewModel>();
        var entity = _mapper.Map<ApprovalGroup>(request);
        _dbContext.ApprovalGroups.Add(entity);

        var members = await _dbContext.ApplicationUsers
          .Where(x => request.ApprovalGroupMemberIds.Contains(x.Id))
          .ToListAsync(cancellationToken);

        var groupMembers = new List<ApprovalGroupMember>();
        foreach (var member in members)
            groupMembers.Add(new ApprovalGroupMember
            {
                ApprovalGroupId = entity.Id,
                CreatedByUserId = request.CreatedByUserId,
                ApplicationUserId = member.Id
            });


        if (groupMembers.Any())
            await _dbContext.ApprovalGroupMembers.AddRangeAsync(groupMembers, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        rsp.Response = new ApprovalGroupViewModel
        {
            Name = request.Name,
            Id = entity.Id,
            MemberCount = groupMembers.Count
        };
        
        rsp.StatusCode = (int)HttpStatusCode.Created;
        rsp.Message = "Approval group created and members added.";
        return rsp;
    }

    public async Task<CommandResult<string>> Handle(DeleteApprovalGroupMemberCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        var appUser = await _dbContext.ApplicationUsers.FindAsync(request.Id);

        var groupMember = await _dbContext.ApprovalGroupMembers.FirstOrDefaultAsync(u => u.ApplicationUserId == appUser.Id, cancellationToken: cancellationToken);

        groupMember.IsDeleted = true;
        
        _dbContext.ApprovalGroupMembers.Update(groupMember);
        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.StatusCode = (int)HttpStatusCode.OK;
        rsp.Message = "Approval member removed.";
        return rsp;
    }

    public async Task<CommandResult<GetApprovalGroupViewModel>> Handle(GetApprovalGroupCommand request,
      CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<GetApprovalGroupViewModel>();
        var group = await _dbContext.ApprovalGroups
          .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var response = new GetApprovalGroupViewModel
        {
            Id = group.Id,
            Name = group.Name
        };

        var groupMemberIds = await _dbContext.ApprovalGroupMembers
          .Where(x => x.ApprovalGroupId == request.Id)
          .Select(x => x.ApplicationUserId)
          .ToListAsync(cancellationToken);

        var groupMembers = _dbContext.MemberProfiles.Where(x => groupMemberIds.Contains(x.ApplicationUserId))
            .Select(y => new ApprovalGroupMemberViewModel
        {
            Id = y.Id,
            FirstName = y.FirstName,
            LastName = y.LastName,
            MiddleName = y.MiddleName,
            MemberId = y.MembershipId,
            Email = y.PrimaryEmail,
            DepartmentId = y.DepartmentId,
            ApplicationUserId = y.ApplicationUserId
        }).ToList() ?? new List<ApprovalGroupMemberViewModel>();


        response.Members = groupMembers;
        rsp.Response = response;
        rsp.StatusCode = (int)HttpStatusCode.OK;
        return rsp;
    }


    public Task<CommandResult<IQueryable<ApprovalGroup>>> Handle(QueryApprovalGroupCommand request,CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<IQueryable<ApprovalGroup>>();
        rsp.Response = _dbContext.ApprovalGroups;

        return Task.FromResult(rsp);
    }

    public async Task<CommandResult<ApprovalGroupViewModel>> Handle(UpdateApprovalGroupCommand request,CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<ApprovalGroupViewModel>();

        var entity = await _dbContext.ApprovalGroups.FindAsync(request.Id);
        entity = _mapper.Map(request, entity);
        _dbContext.ApprovalGroups.Update(entity);
        
        var requestGroupMemberIds = _dbContext.ApplicationUsers
            .Where(x => request.ApprovalGroupMemberIds.Contains(x.Id))
            .ToHashSet();
        
        var existingGroupMembers = _dbContext.ApprovalGroupMembers
            .Include(c => c.ApplicationUser)
            .Where(x => x.ApprovalGroupId == request.Id)
            .Select(v => v.ApplicationUser)
            .ToHashSet();
        
        // Get members to be deleted and set isDeleted to true for them
        var membersToRemove = existingGroupMembers.Except(requestGroupMemberIds).Select(x => x.Id).ToList();
        _dbContext.ApprovalGroupMembers.Where(x => membersToRemove.Contains(x.ApplicationUserId))
            .ToList().ForEach(x =>
            {
                x.IsDeleted = true;
                x.DeletedByUserId = request.UpdatedByUserId;
                x.DateDeleted = DateTime.Now;
            });

        // Get members to be added and add them
        var newGroupMembers = requestGroupMemberIds.Except(existingGroupMembers)
            .Select(x => new ApprovalGroupMember
            {
                ApprovalGroupId = entity.Id,
                CreatedByUserId = request.UpdatedByUserId,
                ApplicationUserId = x.Id
            });
        
        _dbContext.ApprovalGroupMembers.AddRange(newGroupMembers);

        await _dbContext.SaveChangesAsync(cancellationToken);

        rsp.Response = new ApprovalGroupViewModel
        {
            Name = request.Name,
            Id = entity.Id,
            MemberCount = request.ApprovalGroupMemberIds.Count
        };

        rsp.StatusCode = (int)HttpStatusCode.Created;
        rsp.Message = "Approval group and required approvers updated.";
        return rsp;
    }
}