using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public partial class CreateSpecialDepositAccountApplicationCommandValidator : AbstractValidator<CreateSpecialDepositAccountApplicationCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateSpecialDepositAccountApplicationCommandValidator> logger;
        public CreateSpecialDepositAccountApplicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSpecialDepositAccountApplicationCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
            RuleFor(p => p.Amount).NotNull(); 
            RuleFor(p => p.InterestRate).NotNull(); 
            RuleFor(p => p.CustomerId).NotEmpty().WithMessage("Required CustomerId.");
            RuleFor(p => p.DepositProductId).NotEmpty().WithMessage("Required DepositProductId.");
            RuleFor(p => p.CreatedByUserId).NotEmpty().WithMessage("Required CreatedByUserId.");

            RuleFor(x => x.Amount).NotEmpty().Must(x => x > 5000).WithMessage("Amount is required and must be greater than  N5,000.00.");
            RuleFor(x => x.ModeOfPayment).IsInEnum().WithMessage("Invalid Mode of Payment");

            RuleFor(x => x.FileName).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
                .WithMessage("Document fileName is required.");

            RuleFor(x => x.MimeType).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
           .WithMessage("Document fileName is required.");

            RuleFor(x => x.FileSize).NotEmpty().When(x => x.ModeOfPayment == DepositFundingSourceType.BANK_TRANSFER)
            .WithMessage("Document fileSize is required.");

            RuleFor(p => p).Custom((data, context) =>
                {
                    var existingAccount = dbContext.SpecialDepositAccounts.Where(r => r.CustomerId == data.CustomerId && r.IsClosed==false).FirstOrDefault();
                    if (existingAccount!=null)
                    {
                        if (existingAccount.DepositProductId == data.DepositProductId)
                            context.AddFailure(
                             new ValidationFailure(nameof(data.CustomerId),
                                 "Multiple Accounts on same product not allowed.", data.CustomerId));
                    }
                    var parentExists = dbContext.DepositProducts.Where(r => r.Id == data.DepositProductId).Any(); 
                    if (!parentExists)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.DepositProductId),
                        "Selected Deposit Product doesn't exist.", data.DepositProductId));

                    }

                   

                });
  }
 } 
    }



