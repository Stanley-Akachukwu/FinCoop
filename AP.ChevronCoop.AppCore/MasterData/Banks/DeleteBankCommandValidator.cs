using AP.ChevronCoop.AppDomain.MasterData.Banks;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Banks;

public partial class DeleteBankCommandValidator : AbstractValidator<DeleteBankCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteBankCommandValidator> logger;
    public DeleteBankCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteBankCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.Banks.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });

    }


}
