using AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanProductCharges;

public class CreateLoanProductChargeCommandValidator : AbstractValidator<CreateLoanProductChargeCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<CreateLoanProductChargeCommandValidator> logger;

  public CreateLoanProductChargeCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<CreateLoanProductChargeCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(pc => pc.ProductId)
      .NotEmpty().WithMessage("Product ID is required.")
      .MaximumLength(40).WithMessage("Product ID must not exceed 40 characters.");

    RuleFor(pc => pc.ChargeId)
      .NotEmpty().WithMessage("Charge ID is required.")
      .MaximumLength(40).WithMessage("Charge ID must not exceed 40 characters.");

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

              var checkName = dbContext.LoanProductCharges.Where(r => r.Name.ToLower() == data.Name.ToLower()
      && r.CodeTypeId == data.CodeTypeId).Any();
              if (checkName)
              {
                  context.AddFailure(
                  new ValidationFailure(nameof(data.Name),
                  "Duplicate names are not allowed.", data.Name));
              }

              var checkCode = dbContext.LoanProductCharges.Where(r => r.Code.ToLower() == data.Code.ToLower()
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