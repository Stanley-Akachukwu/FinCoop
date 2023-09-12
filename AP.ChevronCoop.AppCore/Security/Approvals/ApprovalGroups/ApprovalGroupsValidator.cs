using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using FluentValidation;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalGroups
{

    public class ApprovalGroupsValidator : AbstractValidator<ApprovalGroup>
    {
        public ApprovalGroupsValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("GroupName required.");
            RuleFor(p => p.Id).NotNull().WithMessage("GroupId required.");
        }
    }
}
