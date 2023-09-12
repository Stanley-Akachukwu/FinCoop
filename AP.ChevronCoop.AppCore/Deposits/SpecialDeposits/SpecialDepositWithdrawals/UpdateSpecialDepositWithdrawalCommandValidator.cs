using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public partial class UpdateSpecialDepositWithdrawalCommandValidator : AbstractValidator<UpdateSpecialDepositWithdrawalCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateSpecialDepositWithdrawalCommandValidator> logger;
        public UpdateSpecialDepositWithdrawalCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositWithdrawalCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
		
			
		    RuleFor(p => p.Id).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().Must(x => x > 1000).WithMessage("Amount is required and must be greater than  N1,000.00.");
            RuleFor(x => x.WithdrawalDestinationType).IsInEnum().WithMessage("Invalid destination account.");
            RuleFor(p => p.CustomerDestinationBankAccountId).NotNull().WithMessage("Invalid destination account.");
            RuleFor(x => x.CreatedByUserId).NotEmpty().WithMessage("CreatedByUserId is required.");
            RuleFor(p => p).Custom((data, context) =>
            {

                var sdAccount = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.SpecialDepositSourceAccountId).Include(a=>a.DepositAccount).FirstOrDefault();
                if (sdAccount ==null)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected account does not exist", data.Id));
                }
                if (sdAccount.DepositAccount.LedgerBalance < data.Amount)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Amount),
                    "Insufficient fund for withdrawal", data.Amount));
                }
                /*
                var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                if (!parentExists)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ParentId),
                    "Invalid key.", data.ParentId));

                }

                var checkName = dbContext.SpecialDepositWithdrawals.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositWithdrawals.Where(r => r.Id != data.Id &&
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


