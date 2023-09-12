using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalWorkflows
{
    public class GetApprovalWorkflowByIdCommandValidator: AbstractValidator<GetApprovalWorkflowByIdCommand>
    {
        private readonly ChevronCoopDbContext _dbContext;

        public GetApprovalWorkflowByIdCommandValidator(ChevronCoopDbContext appDbContext)
        {
            _dbContext = appDbContext;
            RuleFor(p => p.Id).NotNull().WithMessage("Required workflow Id.");
            RuleFor(p => p).Custom((data, context) =>
            {
                var workflow = _dbContext.ApprovalWorkflows.Where(w=>w.Id==data.Id).FirstOrDefault(); 
                if (workflow==null) context.AddFailure(new ValidationFailure("Workflows", "Workflow not found."));

               //var checkWorkflowInUse = _dbContext.ApprovalGroupWorkflows.FirstOrDefault(g=>g.ApprovalWorkflowId== data.Id);
               // if (checkWorkflowInUse != null) context.AddFailure(new ValidationFailure("Workflows", "Retrieval for edit not allowed. Workflow is in use."));
            });
        }
    }
}
