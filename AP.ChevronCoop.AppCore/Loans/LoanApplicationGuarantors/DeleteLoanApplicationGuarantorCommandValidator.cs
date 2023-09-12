using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors;

public class DeleteLoanApplicationGuarantorCommandValidator : AbstractValidator<DeleteLoanApplicationGuarantorCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteLoanApplicationGuarantorCommandValidator> logger;

    public DeleteLoanApplicationGuarantorCommandValidator(ChevronCoopDbContext appDbContext,
      ILogger<DeleteLoanApplicationGuarantorCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;

        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {
            var checkId = dbContext.LoanApplicationGuarantors.Any(r => r.Id == data.Id);
            if (!checkId)
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
        });
    }
}