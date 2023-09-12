using AP.ChevronCoop.AppDomain.Customers.CustomerBeneficiaries;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.CustomerBeneficiaries;

public class DeleteCustomerBeneficiaryCommandValidator : AbstractValidator<DeleteCustomerBeneficiaryCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteCustomerBeneficiaryCommandValidator> logger;
    public DeleteCustomerBeneficiaryCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteCustomerBeneficiaryCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.CustomerBeneficiaries.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            /*
            var checkChild = dbContext.ChildTable.Where(r => r.ChildTableId == data.Id).Any();
                    if (checkChild)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Id),
                        "Selected record has dependent records and cannot be deleted", data.Id));

                    }
            */

        });

    }


}