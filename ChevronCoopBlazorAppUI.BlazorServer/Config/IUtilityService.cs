using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Loans.LoanProducts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Config
{
    public interface IUtilityService
    {
        bool HasDuoRole(List<RoleLookup> roles);
        DateTime RepaymentCommencementDate();
        string LoanGuarantorEligibility(VerifyLoanApplicationGuarantorViewModel guarantorVerifyModel, LoanProductViewModel loanProductViewModel, decimal principal);
        string LoanGuarantorEligibilityTopUp(VerifyLoanApplicationGuarantorViewModel guarantorVerifyModel, LoanProductMasterView loanProductViewModel, decimal principal);
    }
}
