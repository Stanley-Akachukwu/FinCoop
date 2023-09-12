using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals;

public class HandleApprovalCommandValidator : AbstractValidator<HandleApprovalCommand>
{
    private readonly ChevronCoopDbContext _appDbContext;
    private readonly ILogger<HandleApprovalCommandValidator> _logger;

    public HandleApprovalCommandValidator(ChevronCoopDbContext appDbContext,
        ILogger<HandleApprovalCommandValidator> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;

        RuleFor(p => p.Comment);
        RuleFor(p => p.ApprovalId).NotNull();
        //RuleFor(p => p.Status)
        //    .IsEnumName(typeof(ApprovalStatus))
        //    .NotEmpty().WithMessage("Approval Status is required.")
        //    .MaximumLength(32).WithMessage("Status length cannot exceed 32 characters.");
        RuleFor(p => p.Status).NotEmpty().WithMessage("Approval Status is required.");
        RuleFor(p => p).Custom((data, context) =>
        {
            var approval = _appDbContext.Approvals.Any(r => r.Id == data.ApprovalId);
            var applicationUser = _appDbContext.ApplicationUsers.Any(r => r.Id == data.ApplicationUserId);
            if (!applicationUser)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ApplicationUserId),
                        "Selected applicationUser does not exist", data.ApplicationUserId));
            }
            if (!approval)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ApprovalId),
                        "Selected approval does not exist", data.ApprovalId));
            }
        });
    }


}

 
