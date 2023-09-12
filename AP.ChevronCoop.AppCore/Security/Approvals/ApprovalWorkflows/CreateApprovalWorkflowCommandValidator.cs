using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalWorkflows
{
    public partial class CreateApprovalWorkflowCommandValidator : AbstractValidator<CreateApprovalWorkflowCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;

        public CreateApprovalWorkflowCommandValidator(ChevronCoopDbContext appDbContext)
        {
            _dbContext = appDbContext;
            RuleFor(p => p.CreatedByUserId).NotNull().WithMessage("Required creator/initiator User ID.");
            RuleFor(p => p.WorkflowName).NotNull().WithMessage("Required approval Workflow name.");
            RuleFor(p => p).Custom((data, context) =>
            {
                var countApprovalGroups = data.ApprovalGroups.Count();
                if (countApprovalGroups < 1)
                {
                    context.AddFailure(new ValidationFailure("ApprovalGroups", "Required approval groups with at least one approver."));
                }
            });
            
            RuleFor(p => p).Custom((data, context) =>
            {
                var workflowExists = _dbContext.ApprovalWorkflows.Any(g => g.WorkflowName.ToLower() == data.WorkflowName.ToLower());
                if (workflowExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(data.WorkflowName), "Duplicate Approval workflow not allowed. Name already taken.", data.WorkflowName));
                }
            });
        }
    }



}

