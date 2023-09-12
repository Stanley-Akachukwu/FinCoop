using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class DepositProductRateValidator : AbstractValidator<CreateDepositProductInterestDTO>
    {
        public DepositProductRateValidator()
        {
            RuleFor(p => p.LowerLimit).GreaterThan(0).WithMessage("Lower Limit must be greater than Zero");
            RuleFor(p => p.UpperLimit).GreaterThan(p => p.LowerLimit).WithMessage("Upper Limit must be greater than Lower Limit");
            RuleFor(p => p.InterestRate).GreaterThan(0).WithMessage("Interest rate must be greater than Zero");



        }
    }
}
