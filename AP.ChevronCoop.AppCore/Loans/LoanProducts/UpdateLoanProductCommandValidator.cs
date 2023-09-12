using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanProducts;

public class UpdateLoanProductCommandValidator : AbstractValidator<UpdateLoanProductCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanProductCommandValidator> logger;

  public UpdateLoanProductCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdateLoanProductCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty();
    RuleFor(p => p.Code)
      .NotEmpty().WithMessage("Code is required.")
      .MaximumLength(64).WithMessage("Code must not exceed 64 characters.");

    RuleFor(p => p.Name)
      .NotEmpty().WithMessage("Name is required.")
      .MaximumLength(256).WithMessage("Name must not exceed 256 characters.");

    RuleFor(p => p.ShortName)
      .NotEmpty().WithMessage("Short Name is required.")
      .MaximumLength(256).WithMessage("Short Name must not exceed 256 characters.");

    RuleFor(p => p.DefaultCurrencyId)
      .MaximumLength(40).WithMessage("Default Currency Id must not exceed 40 characters.");
    RuleFor(p => p.ApprovalWorkFlowId).NotNull();

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanProducts.Where(r => r.Id == data.Id).Any();
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));
      
      if (data.InterestMethod == InterestMethod.COMPOUND)
        context.AddFailure(
          new ValidationFailure(nameof(data.Name),
            "Loan products that support compound interest methods have not been enabled by the admin.", data.Name));

      var checkName = dbContext.LoanProducts.Any(r => r.Name.ToLower() == data.Name.ToLower() && r.Id != data.Id);
      if (checkName)
        context.AddFailure(
          new ValidationFailure(nameof(data.Name),
            "Duplicate names are not allowed.", data.Name));

      var checkCode = dbContext.LoanProducts.Any(r => r.Code.ToLower() == data.Code.ToLower() && r.Id != data.Id);
      if (checkCode)
        context.AddFailure(
          new ValidationFailure(nameof(data.Code),
            "Duplicate codes are not allowed.", data.Code));

      var checkShotName =
        dbContext.LoanProducts.Any(r => r.ShortName.ToLower() == data.ShortName.ToLower() && r.Id != data.Id);
      if (checkShotName)
        context.AddFailure(
          new ValidationFailure(nameof(data.ShortName),
            "Duplicate Short Name are not allowed.", data.ShortName));
    });
  }
}