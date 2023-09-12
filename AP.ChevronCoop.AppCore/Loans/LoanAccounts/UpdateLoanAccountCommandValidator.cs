using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanAccounts;

public class UpdateLoanAccountCommandValidator : AbstractValidator<UpdateLoanAccountCommand>
{
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanAccountCommandValidator> logger;

  public UpdateLoanAccountCommandValidator(ChevronCoopDbContext appDbContext,
    ILogger<UpdateLoanAccountCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;

    RuleFor(p => p.Id).NotEmpty();
    RuleFor(p => p.AccountNo).NotEmpty().MaximumLength(64);
    RuleFor(p => p.LoanApplicationId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.CustomerId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.PrincipalBalanceAccountId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.PrincipalLossAccountId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.InterestBalanceAccountId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.InterestLossAccountId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.ChargesPayableAccountId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.Principal).NotNull();
    RuleFor(p => p.TenureUnit).NotEmpty().MaximumLength(64);
    RuleFor(p => p.TenureValue).NotNull();
    RuleFor(p => p.RepaymentCommencementDate).NotNull();
    RuleFor(p => p.UseSpecialDeposit).NotNull();
    RuleFor(p => p.IsClosed).NotNull();

    RuleFor(p => p).Custom((data, context) =>
    {
      var checkId = dbContext.LoanAccounts.Where(r => r.Id == data.Id).Any();
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

      var checkName = dbContext.LoanAccounts.Where(r => r.Id != data.Id &&
      r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
      if (checkName)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.Name),
          "Duplicate names are not allowed.", data.Name));
      }

      var checkCode = dbContext.LoanAccounts.Where(r => r.Id != data.Id &&
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