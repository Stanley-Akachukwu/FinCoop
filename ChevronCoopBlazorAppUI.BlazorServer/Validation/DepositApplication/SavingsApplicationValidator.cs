
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation.DepositApplication
{
    public class SavingsApplicationValidator : AbstractValidator<SavingsAccountApplicationViewModel>
    {
        public SavingsApplicationValidator() {

            RuleFor(p => p.Amount).NotEmpty().WithMessage("Please specify amount");

        }

    }
}
