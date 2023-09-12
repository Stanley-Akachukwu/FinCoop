using AP.ChevronCoop.AppDomain.Loans.LoanOffSetCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSetCharges
{
    public partial class UpdateLoanOffSetChargeCommandValidator : AbstractValidator<UpdateLoanOffSetChargeCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateLoanOffSetChargeCommandValidator> logger;
        public UpdateLoanOffSetChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateLoanOffSetChargeCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.LoanOffsetId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.OffsetChargeId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.ChargeType).NotEmpty().MaximumLength(64);
            RuleFor(p => p.TotalCharge).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.LoanOffSetCharges.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.LoanOffSetCharges.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.LoanOffSetCharges.Where(r => r.Id != data.Id &&
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