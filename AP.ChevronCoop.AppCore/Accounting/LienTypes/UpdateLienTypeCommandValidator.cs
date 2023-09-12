using AP.ChevronCoop.AppDomain.Accounting.LienTypes;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Accounting.LienTypes;

public partial class UpdateLienTypeCommandValidator : AbstractValidator<UpdateLienTypeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateLienTypeCommandValidator> logger;
    public UpdateLienTypeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateLienTypeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(64).WithMessage("Code must not exceed 64 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must not exceed 128 characters.");





        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.LienTypes.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check Name Exists
            var checkName = dbContext.LienTypes.Any(r => r.Name == data.Name && r.Id != data.Id);
            if (checkName)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                        $"Lien Type name: {data.Name} exist", data.Name));
            }

            // Check Charge Code Exists
            var checkCode = dbContext.LienTypes.Any(r => r.Code == data.Code && r.Id != data.Id);
            if (checkCode)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                        $"Lien Type code: {data.Code} exist", data.Code));
            }

        });

    }


}
