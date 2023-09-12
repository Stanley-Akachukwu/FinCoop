using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class DepositProductBasicInformationValidator : AbstractValidator<CreateDepositProductDTO>
    {
        public DepositProductBasicInformationValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product Name is required");
            RuleFor(p => p.Name).Matches("^[a-zA-Z0-9\\s]+$").WithMessage("Product Name is not valid");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Product Code is required");
            RuleFor(p => p.Code).Matches("^[a-zA-Z0-9\\s]+$").WithMessage("Product Code is not valid");
            //RuleFor(p => p.Tenure).NotEmpty().WithMessage("Tenor is required");
            RuleFor(p => p.DefaultCurrencyId).NotEmpty().WithMessage("Currency is required");
            RuleFor(p => p.BankDepositAccountId).NotEmpty().WithMessage("Account is required");
            //RuleFor(p => p.MinimumAge).NotEmpty().GreaterThan(17).WithMessage("Minimum Age is required and upto 18 years");
            //RuleFor(p => p.MaximumAge).NotEmpty().GreaterThan(17).WithMessage("Maximum Age is required and upto 18 years");
            //RuleFor(p => p.MaximumAge).GreaterThan(p => p.MinimumAge).WithMessage("Maximum Age must be greater than Minimum Age");
            RuleFor(p => p.TenureValue).GreaterThanOrEqualTo(-1).WithMessage("Tenor Value cannot be less than 0");

            RuleFor(p => p.TenureValue)
            .Must((p, tenureValue) => p.Tenure != Tenure.NONE || tenureValue <= 0)
            .WithMessage("Tenure Value cannot be more than 0 if Tenure value is set to NONE");

            RuleFor(p => p.TenureValue)
     .NotEmpty().When(p => p.Tenure != Tenure.NONE)
         .WithMessage("Tenure value must be provided.")
     .GreaterThan(0).When(p => p.Tenure != Tenure.NONE && p.TenureValue == 0)
         .WithMessage("Tenure value must be greater than 0 if Tenure is not set to NONE.");

        }
    }
}
