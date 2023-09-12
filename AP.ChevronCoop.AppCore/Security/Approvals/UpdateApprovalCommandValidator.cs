using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals
{
    public partial class UpdateApprovalCommandValidator : AbstractValidator<UpdateApprovalCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateApprovalCommandValidator> logger;
        public UpdateApprovalCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApprovalCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
            RuleFor(p => p.RequestedByUserId).NotNull();
            RuleFor(p => p.Comment).NotNull();
            RuleFor(p => p.Payload).NotNull();
            RuleFor(p => p.ApprovalWorkflowId).NotNull();
            RuleFor(p => p.Module).NotNull();


            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.Approvals.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }
            });

        }


    }




}
