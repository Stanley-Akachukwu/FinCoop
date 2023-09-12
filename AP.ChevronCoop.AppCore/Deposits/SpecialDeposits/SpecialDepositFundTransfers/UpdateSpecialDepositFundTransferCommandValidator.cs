using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public partial class UpdateSpecialDepositFundTransferCommandValidator : AbstractValidator<UpdateSpecialDepositFundTransferCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateSpecialDepositFundTransferCommandValidator> logger;
        public UpdateSpecialDepositFundTransferCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositFundTransferCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
		    RuleFor(p => p.Id).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty().Must(x => x > 1000).WithMessage("Amount is required and must be greater than  N1,000.00.");
            RuleFor(p => p.DestinationAccountType).IsInEnum().WithMessage("Invalid Mode of Payment");
            RuleFor(p => p.SpecialDepositAccountId).NotNull().WithMessage("Invalid source account.");
            RuleFor(x => x.CreatedByUserId).NotEmpty().WithMessage("CreatedByUserId is required.");
            RuleFor(p => p).Custom((data, context) =>
            {
                var checkId = dbContext.SpecialDepositFundTransfers.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.SpecialDepositFundTransfers.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositFundTransfers.Where(r => r.Id != data.Id &&
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


