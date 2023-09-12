using AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationItems;

public class UpdateLoanApplicationItemCommandValidator : AbstractValidator<UpdateLoanApplicationItemCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanApplicationItemCommandValidator> logger;

  public UpdateLoanApplicationItemCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdateLoanApplicationItemCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty();

    RuleFor(x => x.LoanApplicationId)
      .NotNull().WithMessage("Loan application id is required.");

    RuleFor(x => x.BrandName)
      .NotEmpty().WithMessage("Brand name is required.")
      .MaximumLength(32).WithMessage("Brand name must not exceed 32 characters.");

    RuleFor(x => x.Model)
      .NotEmpty().WithMessage("Model is required.")
      .MaximumLength(32).WithMessage("Model must not exceed 32 characters.");

    RuleFor(x => x.Color)
      .MaximumLength(16).WithMessage("Color must not exceed 16 characters.");

    RuleFor(x => x.ItemType)
      .NotEmpty().WithMessage("Item type is required.");

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanApplicationItems.Where(r => r.Id == data.Id).Any();
      if (!checkId)
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));

      var isExist = dbContext.LoanApplications.Where(x => x.Id == data.LoanApplicationId).Any();
      if (!isExist)
        context.AddFailure(
          new ValidationFailure(nameof(data.LoanApplicationId),
            "Loan appication does not exist.", data.LoanApplicationId));
    });
  }
}