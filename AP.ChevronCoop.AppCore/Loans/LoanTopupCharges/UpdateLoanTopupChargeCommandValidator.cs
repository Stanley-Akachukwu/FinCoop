using AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopupCharges
{
    public partial class UpdateLoanTopupChargeCommandValidator : AbstractValidator<UpdateLoanTopupChargeCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateLoanTopupChargeCommandValidator> logger;
        public UpdateLoanTopupChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateLoanTopupChargeCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.LoanTopupId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.TopupChargeId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.ChargeType).NotEmpty().MaximumLength(64);
            RuleFor(p => p.TotalCharge).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.LoanTopupCharges.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.LoanTopupCharges.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.LoanTopupCharges.Where(r => r.Id != data.Id &&
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
