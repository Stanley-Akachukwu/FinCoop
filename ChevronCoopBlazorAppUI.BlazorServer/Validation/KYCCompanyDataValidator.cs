using ChevronCoop.Web.AppUI.BlazorServer.Data;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class KYCCompanyDataValidator : AbstractValidator<CompanyDataViewModel>
    {
        public KYCCompanyDataValidator()
        {
            RuleFor(p => p.MembershipId).NotEmpty().WithMessage("Membership No. is required");
            RuleFor(p => p.DateOfEmployment).NotEmpty().WithMessage("Date of Employment is required");

        }
    }
}
