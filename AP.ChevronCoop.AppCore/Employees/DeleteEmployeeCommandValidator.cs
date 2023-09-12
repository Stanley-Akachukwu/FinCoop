using AP.ChevronCoop.AppDomain.Employees;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Employees;

public partial class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteEmployeeCommandValidator> logger;
    public DeleteEmployeeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteEmployeeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.Employees.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

        });

    }


}
