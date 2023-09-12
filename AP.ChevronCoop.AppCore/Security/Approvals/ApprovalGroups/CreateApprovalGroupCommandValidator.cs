using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroups
{
    public partial class CreateApprovalGroupCommandValidator : AbstractValidator<CreateApprovalGroupCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<CreateApprovalGroupCommandValidator> _logger;
        public CreateApprovalGroupCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApprovalGroupCommandValidator> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
            RuleFor(p => p.Name).NotNull().WithMessage("CreatedByUserId address required.");
            RuleFor(p => p.CreatedByUserId).NotNull().WithMessage("CreatedByUserId address required.");
            RuleFor(p => p).Custom((data, context) =>
            {

                var users = _dbContext.ApplicationUsers.Where(x => data.ApprovalGroupMemberIds.Contains(x.Id)).ToList();
                if (!users.Any())
                {
                    context.AddFailure(new ValidationFailure(nameof(data.ApprovalGroupMemberIds), "Approvers provided are not found.", data.ApprovalGroupMemberIds));
                }
                
                var approvalGroupExists = _dbContext.ApprovalGroups.FirstOrDefault(g => g.Name == data.Name) == null;
                if (!approvalGroupExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(data.Name), "Duplicate Approval Group not allowed. Name already taken.", data.Name));
                }
            });
        }
    }
}


