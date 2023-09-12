using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalNotifications;

public partial class SendApprovalNotificationCommandValidator : AbstractValidator<SendApprovalRequestNotificationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<SendApprovalNotificationCommandValidator> logger;
    public SendApprovalNotificationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<SendApprovalNotificationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.ApprovalId)
            .NotNull().WithMessage("Approval ID is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

        });

    }


}
