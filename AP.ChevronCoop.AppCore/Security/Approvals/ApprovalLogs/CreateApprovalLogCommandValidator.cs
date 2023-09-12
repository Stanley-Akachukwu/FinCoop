using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalLogs;

public class CreateApprovalLogCommandValidator : AbstractValidator<CreateApprovalLogCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<CreateApprovalLogCommandValidator> logger;

  public CreateApprovalLogCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<CreateApprovalLogCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(p => p.ApprovalId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.Sequence).NotNull();
    RuleFor(p => p.ApprovalGroupId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.ApprovedByUserId).NotEmpty().MaximumLength(900);
    RuleFor(p => p.DateApproved).NotNull();
    RuleFor(p => p.Status).NotEmpty().MaximumLength(64);


    RuleFor(p => p).Custom((data, context) =>
    {
      /*
            var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
            if (!parentExists)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.ParentId),
                "Invalid key.", data.ParentId));

            }

            var checkName = dbContext.ApprovalLogs.Where(r => r.Name.ToLower() == data.Name.ToLower() 
    && r.CodeTypeId == data.CodeTypeId).Any();
            if (checkName)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Name),
                "Duplicate names are not allowed.", data.Name));
            }

            var checkCode = dbContext.ApprovalLogs.Where(r => r.Code.ToLower() == data.Code.ToLower() 
    && r.CodeTypeId != data.CodeTypeId).Any();
            if (checkCode)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Code),
                "Duplicate codes are not allowed.", data.Code));
            }
    */
    });
  }
}