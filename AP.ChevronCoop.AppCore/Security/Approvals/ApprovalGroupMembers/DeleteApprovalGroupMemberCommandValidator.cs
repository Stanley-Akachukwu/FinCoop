using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroupMembers
{
    public partial class DeleteApprovalGroupMemberCommandValidator : AbstractValidator<DeleteApprovalGroupMemberCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<DeleteApprovalGroupMemberCommandValidator> _logger;
        public DeleteApprovalGroupMemberCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteApprovalGroupMemberCommandValidator> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
            RuleFor(p => p.ApprovalGroupId).NotNull().WithMessage("Approval GroupId required.");
            RuleFor(p => p.DeletedByUserId).NotNull().WithMessage("DeleteByUserId address required.");
            RuleFor(p => p.Id).NotNull().WithMessage("Entity Id required.");
        }
    }
}
