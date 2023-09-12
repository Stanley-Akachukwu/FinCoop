using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
namespace AP.ChevronCoop.AppCore.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public partial class UpdateSpecialDepositAccountCommandValidator : AbstractValidator<UpdateSpecialDepositAccountCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateSpecialDepositAccountCommandValidator> logger;
        public UpdateSpecialDepositAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateSpecialDepositAccountCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;
		 RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.FundingAmount).NotNull(); 
        RuleFor(p => p.InterestRate).NotNull(); 
        RuleFor(p => p.LastInterestComputationDate).NotNull(); 
        RuleFor(p => p.MaximumBalanceLimit).NotNull(); 
        RuleFor(p => p.MinimumBalanceLimit).NotNull(); 
        RuleFor(p => p.SingleWithdrawalLimit).NotNull(); 
        RuleFor(p => p.DailyWithdrawalLimit).NotNull(); 
        RuleFor(p => p.WeeklyWithdrawalLimit).NotNull(); 
        RuleFor(p => p.MonthlyWithdrawalLimit).NotNull(); 
        RuleFor(p => p.IsClosed).NotNull(); 
     RuleFor(p => p).Custom((data, context) =>
                {

                var checkId = dbContext.SpecialDepositAccounts.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.SpecialDepositAccounts.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.SpecialDepositAccounts.Where(r => r.Id != data.Id &&
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


