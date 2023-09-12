using AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroupMembers;

public class CreateApprovalGroupMemberCommandValidator : AbstractValidator<CreateApprovalGroupMemberCommand>
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly ILogger<CreateApprovalGroupCommandValidator> _logger;
    public CreateApprovalGroupMemberCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApprovalGroupCommandValidator> logger)
    {
        _dbContext = appDbContext;
        _logger = logger;
        RuleFor(p => p.ApprovalGroupId).NotNull().WithMessage("Approval Group Id is required.");
        RuleFor(p => p.MemberProfileId).NotNull().WithMessage("Member Profile Id is required.");
        RuleFor(p => p).Custom((data, context) =>
        {
            //var approvalGroupMemberExists = _dbContext.ApprovalGroupMembers.Any(g =>
            //    g.CustomerId == data.MemberProfileId && g.ApprovalGroupId == data.ApprovalGroupId);

            var approvalGroupMemberExists = _dbContext.ApprovalGroupMembers.Any(g =>
                g.ApplicationUserId == data.MemberProfileId && g.ApprovalGroupId == data.ApprovalGroupId);

            if (approvalGroupMemberExists)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ApprovalGroupId), "Selected member is part of the group already.", data.ApprovalGroupId));
            }
        });
    }
}