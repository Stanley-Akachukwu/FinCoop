using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.Customers;

public partial class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteCustomerCommandValidator> logger;
    public DeleteCustomerCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteCustomerCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.Customers.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
