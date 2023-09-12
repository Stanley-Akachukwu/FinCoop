using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroupMembers
{
    public partial class UpdateApprovalGroupMemberCommandValidator : AbstractValidator<CreateOrUpdateGroupMemberCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<UpdateApprovalGroupMemberCommandValidator> _logger;
        public UpdateApprovalGroupMemberCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApprovalGroupMemberCommandValidator> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
            RuleFor(p => p.CreatedByUserId).NotNull().WithMessage("CreatedByUserId address required.");
            RuleFor(p => p.MembershipId).NotNull().WithMessage("MembershipId address required.");
        }
    }
}
