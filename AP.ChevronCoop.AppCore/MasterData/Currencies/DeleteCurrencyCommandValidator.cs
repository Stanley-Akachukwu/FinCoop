using AP.ChevronCoop.AppDomain.MasterData.Currencies;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;


namespace AP.ChevronCoop.AppCore.MasterData.Currencies
{
    public partial class DeleteCurrencyCommandValidator : AbstractValidator<DeleteCurrencyCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteCurrencyCommandValidator> logger;
        public DeleteCurrencyCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteCurrencyCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.Currencies.Any(r => r.Id == data.Id);
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }
            });

        }


    }


}
