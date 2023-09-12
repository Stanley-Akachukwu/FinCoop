using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

public class UpdatePayrollDeductionItemCommandValidator : AbstractValidator<UpdatePayrollDeductionItemCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdatePayrollDeductionItemCommandValidator> logger;

  public UpdatePayrollDeductionItemCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdatePayrollDeductionItemCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty();
    RuleFor(p => p.PayrollDeductionScheduleId).NotEmpty().MaximumLength(80);

    RuleFor(p => p.MemberId).NotEmpty().MaximumLength(80);

    RuleFor(p => p.MemberName).NotEmpty().MaximumLength(240);
    RuleFor(p => p.AccountNo).NotEmpty().MaximumLength(64);
    RuleFor(p => p.Amount).NotNull();
    RuleFor(p => p.PayrollCode).NotEmpty().MaximumLength(510);
    RuleFor(p => p.Narration).NotEmpty().MaximumLength(510);
    RuleFor(p => p.PayrollDate).NotNull();

    RuleFor(p => p.AccountDueDate).NotNull();
    RuleFor(p => p.DeductionType).NotEmpty().MaximumLength(64);


    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.PayrollDeductionItems.Any(r => r.Id == data.Id);
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));

      /*
      var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
      if (!parentExists)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.ParentId),
          "Invalid key.", data.ParentId));

      }

      var checkName = dbContext.PayrollDeductionItems.Where(r => r.Id != data.Id &&
      r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
      if (checkName)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.Name),
          "Duplicate names are not allowed.", data.Name));
      }

      var checkCode = dbContext.PayrollDeductionItems.Where(r => r.Id != data.Id &&
      r.Code.ToLower() == data.Code.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
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