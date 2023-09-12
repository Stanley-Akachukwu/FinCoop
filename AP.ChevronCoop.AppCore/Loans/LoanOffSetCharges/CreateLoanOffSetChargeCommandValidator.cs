using AP.ChevronCoop.AppDomain.Loans.LoanOffsetCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSetCharges
{
    public partial class CreateLoanOffSetChargeCommandValidator : AbstractValidator<CreateLoanOffSetChargeCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateLoanOffSetChargeCommandValidator> logger;
        public CreateLoanOffSetChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateLoanOffSetChargeCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.LoanOffsetId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.OffsetChargeId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.ChargeType).NotEmpty().MaximumLength(64);
            RuleFor(p => p.TotalCharge).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {


                /*
                        var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                        if (!parentExists)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.ParentId),
                            "Invalid key.", data.ParentId));

                        }

                        var checkName = dbContext.LoanOffSetCharges.Where(r => r.Name.ToLower() == data.Name.ToLower() 
                && r.CodeTypeId == data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.LoanOffSetCharges.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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