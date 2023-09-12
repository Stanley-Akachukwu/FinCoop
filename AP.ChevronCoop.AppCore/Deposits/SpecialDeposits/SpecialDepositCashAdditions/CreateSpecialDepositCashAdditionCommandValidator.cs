using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public partial class CreateSpecialDepositCashAdditionCommandValidator : AbstractValidator<CreateSpecialDepositCashAdditionCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateSpecialDepositCashAdditionCommandValidator> logger;
        public CreateSpecialDepositCashAdditionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSpecialDepositCashAdditionCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
            RuleFor(x => x.Amount).NotEmpty().Must(x => x > 0).WithMessage("Amount is required and must be greater than  0.");
            RuleFor(x => x.ModeOfPayment).IsInEnum().WithMessage("Invalid Mode of Payment");

            RuleFor(x => x.FileName).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
                .WithMessage("Document fileName is required.");

            RuleFor(x => x.MimeType).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
           .WithMessage("Document fileName is required.");

            RuleFor(x => x.FileSize).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
            .WithMessage("Document fileSize is required.");
            RuleFor(x => x.FileSize).NotEmpty().Must(x => x > 0).WithMessage("File size must not be null.");

            RuleFor(x => x.CreatedByUserId).NotEmpty().WithMessage("CreatedByUserId is required.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var parentExists = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.SpecialDepositAccountId).Any();
                if (!parentExists)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.SpecialDepositAccountId),
                    "Invalid key.", data.SpecialDepositAccountId));
                }

                /*
                var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                if (!parentExists)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ParentId),
                    "Invalid key.", data.ParentId));

                }

                var checkName = dbContext.SpecialDepositCashAdditions.Where(r => r.Name.ToLower() == data.Name.ToLower() 
				&& r.CodeTypeId == data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositCashAdditions.Where(r => r.Code.ToLower() == data.Code.ToLower() 
				&& r.CodeTypeId != data.CodeTypeId).Any();
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



