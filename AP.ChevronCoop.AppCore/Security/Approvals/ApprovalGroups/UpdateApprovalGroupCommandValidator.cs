using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroups
{
    public partial class UpdateApprovalGroupCommandValidator : AbstractValidator<UpdateApprovalGroupCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<UpdateApprovalGroupCommandValidator> _logger;
        public UpdateApprovalGroupCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApprovalGroupCommandValidator> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
            RuleFor(p => p.Name).NotNull().WithMessage("Approval group name required.");
            RuleFor(p => p.Id).NotNull().WithMessage("ApprovalGroupId required.");
            RuleFor(p => p.UpdatedByUserId).NotNull().WithMessage("UpdatedByUserId address required.");
            RuleFor(p => p).Custom((data, context) =>
            {
                var entity = _dbContext.ApprovalGroups.Find(data.Id);
                if (entity == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(data.Name), "Approval Group not found.", data.Name));
                }
                var users = _dbContext.ApplicationUsers.Where(x => data.ApprovalGroupMemberIds.Contains(x.Id)).ToList();
                if (!users.Any())
                {
                    context.AddFailure(new ValidationFailure(nameof(data.ApprovalGroupMemberIds), "Required Approvers.", data.ApprovalGroupMemberIds));

                }

            });
        }
    }
}


