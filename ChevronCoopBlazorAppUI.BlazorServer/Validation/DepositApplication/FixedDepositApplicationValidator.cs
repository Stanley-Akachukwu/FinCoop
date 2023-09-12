using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation.DepositApplication
{
    public class FixedDepositApplicationValidator : AbstractValidator<CreateFixedDepositAccountApplicationCommandFE>
    {
        public FixedDepositApplicationValidator()
        {

            RuleFor(p => p.Amount).NotEmpty().WithMessage("Please specify amount");
            RuleFor(p => p.Amount).GreaterThanOrEqualTo(1).WithMessage("Loan Amount cannot be less than product minimum amount");
        }
    }
}
