using AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanDisbursementCharges;

public partial class CreateLoanDisbursementChargeCommandValidator : AbstractValidator<CreateLoanDisbursementChargeCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateLoanDisbursementChargeCommandValidator> logger;
    public CreateLoanDisbursementChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateLoanDisbursementChargeCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.LoanDisbursementId).NotEmpty().MaximumLength(80);
        RuleFor(p => p.DisbursementChargeId).NotEmpty().MaximumLength(80);
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

                    var checkName = dbContext.LoanDisbursementCharges.Where(r => r.Name.ToLower() == data.Name.ToLower() 
            && r.CodeTypeId == data.CodeTypeId).Any();
                    if (checkName)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                        "Duplicate names are not allowed.", data.Name));
                    }

                    var checkCode = dbContext.LoanDisbursementCharges.Where(r => r.Code.ToLower() == data.Code.ToLower() 
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