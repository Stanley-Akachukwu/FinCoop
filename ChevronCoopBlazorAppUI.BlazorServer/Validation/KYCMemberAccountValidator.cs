using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class KYCMemberAccountValidator : AbstractValidator<MemberAccountViewModel>
    {
        public KYCMemberAccountValidator()
        {
            RuleFor(p => p.AccountNumber).NotEmpty().WithMessage("AccountName No. is required");
            RuleFor(p => p.BankId).NotEmpty().WithMessage("Bank Name Is Required!");
            RuleFor(p => p.AccountName).NotEmpty().WithMessage("Account Name Is Required!");
            RuleFor(p => p.Branch).NotEmpty().WithMessage("Branch Is Required!");
            RuleFor(p => p.AccountNumber).Matches("^\\d{10}$").WithMessage("Account No. is not valid");

        }
    }
}
