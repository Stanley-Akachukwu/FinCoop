using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public partial class UpdateSpecialDepositDefaultCreatedAccountCommandValidator : AbstractValidator<UpdateSpecialDepositDefaultCreatedAccountCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateSpecialDepositDefaultCreatedAccountCommandValidator> logger;
        public UpdateSpecialDepositDefaultCreatedAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositDefaultCreatedAccountCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;
            RuleFor(p => p.FundingAmount).NotNull();
            RuleFor(p => p.SpecialDepositAccountId).NotNull();
            RuleFor(p => p.PaymentAccountNumber).NotNull();
            RuleFor(p => p.PaymentBankName).NotNull();
            RuleFor(p => p.UpdatedByUserId).NotNull();

            RuleFor(x => x.ModeOfPayment).IsInEnum().WithMessage("Invalid Mode of Payment");

           // RuleFor(x => x.FileName).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
           //     .WithMessage("Document fileName is required.");

           // RuleFor(x => x.MimeType).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
           //.WithMessage("Document fileName is required.");

           // RuleFor(x => x.FileSize).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
           // .WithMessage("Document fileSize is required.");

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.SpecialDepositAccountId).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.SpecialDepositAccountId),
                    "Selected DepositAccountId does not exist", data.SpecialDepositAccountId));
                }
                if(data.ModeOfPayment != DepositFundingSourceType.PAYROLL)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.SpecialDepositAccountId),
                    "Selected Funding Source Type must be Payroll.", data.SpecialDepositAccountId));
                }

            });

        }


    }
}


