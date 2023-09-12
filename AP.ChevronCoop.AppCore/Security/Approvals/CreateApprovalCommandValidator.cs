using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals
{
    public partial class CreateApprovalCommandValidator : AbstractValidator<CreateApprovalCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateApprovalCommandValidator> logger;
        public CreateApprovalCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApprovalCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.RequestedByUserId).NotNull();
            RuleFor(p => p.Comment);
            RuleFor(p => p.Payload).NotNull();
            RuleFor(p => p.ApprovalWorkflowId).NotNull();
            RuleFor(p => p.Module).NotNull();




            RuleFor(p => p).Custom((data, context) =>
            {
                var approvalWorkflow = dbContext.ApprovalWorkflows.Any(r => r.Id == data.ApprovalWorkflowId);
                if (!approvalWorkflow)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.ApprovalWorkflowId),
                            "Selected approval workflow does not exist", data.ApprovalWorkflowId));
                }
            });

        }


    }




}
