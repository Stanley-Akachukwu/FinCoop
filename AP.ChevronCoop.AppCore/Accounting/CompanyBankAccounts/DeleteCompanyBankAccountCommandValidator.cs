using AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.CompanyBankAccounts;

public partial class DeleteCompanyBankAccountCommandValidator : AbstractValidator<DeleteCompanyBankAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteCompanyBankAccountCommandValidator> logger;
    public DeleteCompanyBankAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteCompanyBankAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.CompanyBankAccounts.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
