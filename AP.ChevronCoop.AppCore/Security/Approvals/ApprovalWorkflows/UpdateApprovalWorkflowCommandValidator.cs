using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalWorkflows
{
    public partial class UpdateApprovalWorkflowCommandValidator : AbstractValidator<UpdateApprovalWorkflowCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;
        public UpdateApprovalWorkflowCommandValidator(ChevronCoopDbContext appDbContext)
        {
            _dbContext = appDbContext;
            RuleFor(p => p.Id).NotNull().WithMessage("Required WorkflowId.");

            RuleFor(p => p.UpdatedByUserId).NotNull().WithMessage("Required UpdateByUserId.");
            RuleFor(p => p.WorkflowName).NotNull().WithMessage("Required approval Workflow name.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var workflow = _dbContext.ApprovalWorkflows.FirstOrDefault(m => m.Id == data.Id);

                if (workflow == null) context.AddFailure(new ValidationFailure(nameof(data.Id), "Approval workflow Doesn't exist", data.Id));

            });
        }
    }
}
