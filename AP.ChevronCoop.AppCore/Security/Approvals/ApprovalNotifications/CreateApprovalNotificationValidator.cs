using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalNotifications
{
    public class CreateApprovalNotificationValidator : AbstractValidator<CreateApprovalNotificationCommand>
    {
        public CreateApprovalNotificationValidator()
        {
            RuleFor(p => p.MaxReminderCount).NotNull().WithMessage("Maximum reminder count not provided.");
            RuleFor(p => p.ReminderTriggerTime).NotNull().WithMessage("Reminder trigger not provided.");
            RuleFor(p => p.ApprovalWorkflowId).NotNull().WithMessage("ApprovalWorkflowId not provided.");
            RuleFor(p => p.EscalateToUserIds).NotNull().WithMessage("User to escalate to not provided.");
        }
    }
}


