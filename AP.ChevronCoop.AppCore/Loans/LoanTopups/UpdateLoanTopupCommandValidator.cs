using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopups
{
    public partial class UpdateLoanTopupCommandValidator : AbstractValidator<UpdateLoanTopupCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateLoanTopupCommandValidator> logger;
        public UpdateLoanTopupCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateLoanTopupCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.LoanAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.TopupAmount).NotNull();
            RuleFor(p => p.DestinationType).NotEmpty().MaximumLength(64);
            RuleFor(p => p.SpecialDepositAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.CustomerBankAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.OldPrincipalBalance).NotNull();
            RuleFor(p => p.NewPrincipalBalance).NotNull();
            RuleFor(p => p.OldInterestBalance).NotNull();
            RuleFor(p => p.NewInterestBalance).NotNull();
            RuleFor(p => p.TotalTopupCharges).NotNull();
            RuleFor(p => p.TopupDate).NotNull();
            RuleFor(p => p.CommencementDate).NotNull();
            RuleFor(p => p.TransactionJournalId).NotEmpty().MaximumLength(80);

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.LoanTopups.Where(r => r.Id == data.Id).Any();
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

              var checkName = dbContext.LoanTopups.Where(r => r.Id != data.Id &&
              r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
              if (checkName)
              {
                  context.AddFailure(
                  new ValidationFailure(nameof(data.Name),
                  "Duplicate names are not allowed.", data.Name));
              }

              var checkCode = dbContext.LoanTopups.Where(r => r.Id != data.Id &&
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
