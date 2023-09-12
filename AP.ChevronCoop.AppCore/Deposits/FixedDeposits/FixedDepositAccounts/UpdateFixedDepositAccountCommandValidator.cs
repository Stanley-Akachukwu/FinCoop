using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace ChevronCoop.API.Controllers.Deposits.FixedDeposits.FixedDepositAccounts
{
    public partial class UpdateFixedDepositAccountCommandValidator : AbstractValidator<UpdateFixedDepositAccountCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateFixedDepositAccountCommandValidator> logger;
        public UpdateFixedDepositAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateFixedDepositAccountCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.AccountNo).NotEmpty().MaximumLength(100);
            RuleFor(p => p.CustomerId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.DepositProductId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.DepositAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.ChargesAccruedAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.InterestEarnedAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.InterestPayoutAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.Amount).NotNull();
            RuleFor(p => p.TenureUnit).NotEmpty().MaximumLength(128);
            RuleFor(p => p.TenureValue).NotNull();
            RuleFor(p => p.InterestRate).NotNull();
            RuleFor(p => p.MaturityInstructionType).NotEmpty().MaximumLength(-1);
            RuleFor(p => p.LiquidationAccountType).NotEmpty().MaximumLength(-1);



            RuleFor(p => p.IsClosed).NotNull();

            RuleFor(p => p.MaximumBalanceLimit).NotNull();
            RuleFor(p => p.MinimumBalanceLimit).NotNull();
            RuleFor(p => p.SingleWithdrawalLimit).NotNull();
            RuleFor(p => p.DailyWithdrawalLimit).NotNull();
            RuleFor(p => p.WeeklyWithdrawalLimit).NotNull();
            RuleFor(p => p.MonthlyWithdrawalLimit).NotNull();






            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.FixedDepositAccounts.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }

                /*
                var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                if (!parentExists)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ParentId),
                    "Invalid key.", data.ParentId));

                }

                var checkName = dbContext.FixedDepositAccounts.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.FixedDepositAccounts.Where(r => r.Id != data.Id &&
                r.Code.ToLower() == data.Code.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkCode)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Code),
                    "Duplicate codes are not allowed.", data.Code));
                }
                */

            });

        }


    }
}