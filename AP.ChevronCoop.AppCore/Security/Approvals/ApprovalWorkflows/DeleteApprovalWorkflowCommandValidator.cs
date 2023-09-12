using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalWorkflows
{
    public class DeleteApprovalWorkflowCommandValidator : AbstractValidator<DeleteApprovalWorkflowCommand>
    {
        public DeleteApprovalWorkflowCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p).Custom((data, context) =>
            {
            });
        }
    }
}
