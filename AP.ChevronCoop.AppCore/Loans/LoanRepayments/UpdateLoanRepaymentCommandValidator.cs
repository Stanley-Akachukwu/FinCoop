using AP.ChevronCoop.AppDomain.Loans.LoanRepayments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanRepayments
{
    public partial class UpdateLoanRepaymentCommandValidator : AbstractValidator<UpdateLoanRepaymentCommand>
    {
  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<UpdateLoanRepaymentCommandValidator> logger;
        public UpdateLoanRepaymentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateLoanRepaymentCommandValidator> _logger)
  {
    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty();
    RuleFor(p => p.Status).NotEmpty().MaximumLength(64);
    RuleFor(p => p.RepaymentMode).NotEmpty().MaximumLength(64);
    RuleFor(p => p.LoanAccountId).NotEmpty().MaximumLength(80);
    RuleFor(p => p.LoanRepaymentScheduleId).NotEmpty().MaximumLength(80);


    RuleFor(p => p.Amount).NotNull();
    RuleFor(p => p.Principal).NotNull();
    RuleFor(p => p.Interest).NotNull();


    RuleFor(p => p.TransactionJournalId).NotEmpty().MaximumLength(80);


    RuleFor(p => p).Custom((data, context) =>
    {

      var checkId = dbContext.LoanRepayments.Where(r => r.Id == data.Id).Any();
      if (!checkId)
                {
        context.AddFailure(
          new ValidationFailure(nameof(data.Id),
            "Selected Id does not exist", data.Id));
                }

      /*
      var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
      if (!parentExists)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.ParentId),
          "Invalid key.", data.ParentId));

      }

      var checkName = dbContext.LoanRepayments.Where(r => r.Id != data.Id &&
      r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
      if (checkName)
      {
          context.AddFailure(
          new ValidationFailure(nameof(data.Name),
          "Duplicate names are not allowed.", data.Name));
      }

      var checkCode = dbContext.LoanRepayments.Where(r => r.Id != data.Id &&
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
}