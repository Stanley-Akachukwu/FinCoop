
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccounts;

public partial class UpdateSavingsAccountCommandValidator : AbstractValidator<UpdateSavingsAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateSavingsAccountCommandValidator> logger;
    public UpdateSavingsAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSavingsAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.AccountNo).NotEmpty().MaximumLength(100);

        RuleFor(p => p.PayrollAmount).NotNull();
        RuleFor(p => p.IsClosed).NotNull();

        RuleFor(p => p.MaximumBalanceLimit).NotNull();
        RuleFor(p => p.MinimumBalanceLimit).NotNull();
        RuleFor(p => p.SingleWithdrawalLimit).NotNull();
        RuleFor(p => p.DailyWithdrawalLimit).NotNull();
        RuleFor(p => p.WeeklyWithdrawalLimit).NotNull();
        RuleFor(p => p.MonthlyWithdrawalLimit).NotNull();


        RuleFor(p => p).Custom((data, context) =>
        {
            var checkId = dbContext.SavingsAccounts.Where(r => r.Id == data.Id).Any();
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }


        });

    }


}



