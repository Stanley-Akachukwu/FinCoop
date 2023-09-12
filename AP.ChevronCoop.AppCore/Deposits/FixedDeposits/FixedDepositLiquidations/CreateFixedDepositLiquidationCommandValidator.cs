using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.FixedDeposits.FixedDepositLiquidations;
public partial class CreateFixedDepositLiquidationCommandValidator : AbstractValidator<CreateFixedDepositLiquidationCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateFixedDepositLiquidationCommandValidator> logger;
    public CreateFixedDepositLiquidationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateFixedDepositLiquidationCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.CustomerId).NotEmpty().WithMessage("Customer Id is required");
        RuleFor(p => p.FixedDepositAccountId).NotEmpty().WithMessage("Account is required");
        RuleFor(x => x.LiquidationAccountType).IsInEnum().WithMessage("Invalid liquidation account type.");

        RuleFor(p => p).Custom((data, context) =>
        {


            var customer = dbContext.Customers.Where(r => r.Id == data.CustomerId).FirstOrDefault();

            if (customer == null)
            {
                context.AddFailure(
                     new ValidationFailure(nameof(data.CustomerId),
                         "Customer not found.", data.CustomerId));
            }



            var fixedDepositLiquidation = dbContext.FixedDepositLiquidations.Where(r => r.FixedDepositAccountId == data.FixedDepositAccountId)
           .FirstOrDefault();

            if (fixedDepositLiquidation != null)
            {
                context.AddFailure(
                     new ValidationFailure(nameof(data.FixedDepositAccountId),
                         "Liquidation request already exist for this request.", data.CustomerId));
            }


            var FixedDepositaccount = dbContext.FixedDepositAccounts.Where(r => r.Id == data.FixedDepositAccountId && r.CustomerId == data.CustomerId)
            .FirstOrDefault();

            if (FixedDepositaccount == null)
            {
                context.AddFailure(
                     new ValidationFailure(nameof(data.CustomerId),
                         "Fixed Deposit not found.", data.CustomerId));
            }


            if (data.LiquidationAccountType == WithdrawalAccountType.SPECIAL_DEPOSIT_ACCOUNT)
            {
                var account = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.LiquidationAccountId && r.CustomerId == data.CustomerId)
               .FirstOrDefault();

                if (account == null)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.LiquidationAccountId),
                            "Special Deposit Account doesn't exist for customer", data.LiquidationAccountId));
                }

            }

            if (data.LiquidationAccountType == WithdrawalAccountType.SAVINGS_ACCOUNT)
            {
                var account = dbContext.SavingsAccounts.Where(r => r.Id == data.LiquidationAccountId && r.CustomerId == data.CustomerId)
               .FirstOrDefault();

                if (account == null)
                {

                    context.AddFailure(
                        new ValidationFailure(nameof(data.LiquidationAccountId),
                            "Savings Account doesn't exist for customer", data.LiquidationAccountId));
                }
            }


            if (data.LiquidationAccountType == WithdrawalAccountType.EXISTING_BANK_ACCOUNT)
            {
                var account = dbContext.CustomerBankAccounts.Where(r => r.Id == data.LiquidationAccountId && r.CustomerId == data.CustomerId)
               .FirstOrDefault();

                if (account == null)
                {


                    context.AddFailure(
                    new ValidationFailure(nameof(data.LiquidationAccountId),
                        "Bank Account doesn't exist for customer", data.LiquidationAccountId));
                }

            }


        });
    }


}



