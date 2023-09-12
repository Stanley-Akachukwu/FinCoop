using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation.DepositApplication
{
    public class SpecialDepositValidator : AbstractValidator<SpecialDepositAccountApplication>
    {
        public SpecialDepositValidator()
        {
            RuleFor(p => p.Amount).NotEmpty().WithMessage("Please specify amount");
            // RuleFor(p => p.Amount).LessThanOrEqualTo(50000).WithMessage("Amount cannot be less than N50,000");

        }
    }
}
