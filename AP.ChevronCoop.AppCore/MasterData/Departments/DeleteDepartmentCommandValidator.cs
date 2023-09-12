using AP.ChevronCoop.AppDomain.MasterData.Departments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Departments;

public partial class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteDepartmentCommandValidator> logger;
    public DeleteDepartmentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteDepartmentCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.Departments.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });

    }


}
