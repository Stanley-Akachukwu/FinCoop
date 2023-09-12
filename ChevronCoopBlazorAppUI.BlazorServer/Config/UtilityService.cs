using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Loans.LoanProducts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Config
{
    public class UtilityService : IUtilityService
    {
        public static decimal LOAN_GUARANTOR_CAP = 50000000;
        public UtilityService()
        {

        }
        public bool HasDuoRole(List<RoleLookup> roles)
        {
            var HasStaffRoleAndMemberRole = roles.Where(f => f.Name.ToLower() == "regular" || f.Name.ToLower() == "retiree" || f.Name.ToLower() == "expatriate").ToList();
            if (roles.Count > 1 && HasStaffRoleAndMemberRole.Count > 0)
                return true;
            return false;
        }
        public DateTime RepaymentCommencementDate()
        {
            DateTime currentDate = DateTime.Now;
            // Get the current month
            int currentMonth = currentDate.Month;

            // Get the current year
            int currentYear = currentDate.Year;

            // Create a new DateTime instance for the 15th of the current month
            DateTime next15th = new DateTime(currentYear, currentMonth, 1);

            // If the current date is already the 15th or later, add one month
            if (currentDate.Day >= 15)
            {
                next15th = next15th.AddMonths(1);
            }

            return next15th;
        }
        public string LoanGuarantorEligibility(VerifyLoanApplicationGuarantorViewModel guarantorVerifyModel, LoanProductViewModel loanProductViewModel, decimal principal)
        {
            if (guarantorVerifyModel == null || loanProductViewModel == null)
                return "Please select all required fields to continue!";
            else if ((guarantorVerifyModel.TotalRunningLoan + principal) > LOAN_GUARANTOR_CAP)
                return $"This guarantor already have {guarantorVerifyModel.TotalRunningLoan.ToString("N2")} running loan";
            else if (guarantorVerifyModel.YearsOfService < loanProductViewModel.GuarantorMinYear)
                return $"Guarantor's year of service ({guarantorVerifyModel.YearsOfService}) is less than the minimum ({loanProductViewModel.GuarantorMinYear}) for this loan";
            else
                return null;
        }

        public string LoanGuarantorEligibilityTopUp(VerifyLoanApplicationGuarantorViewModel guarantorVerifyModel, LoanProductMasterView loanProductViewModel, decimal principal)
        {
            if (guarantorVerifyModel == null || loanProductViewModel == null)
                return "Please select all required fields to continue!";
            else if ((guarantorVerifyModel.TotalRunningLoan + principal) > LOAN_GUARANTOR_CAP)
                return $"This guarantor already have {guarantorVerifyModel.TotalRunningLoan.ToString("N2")} running loan";
            else if (guarantorVerifyModel.YearsOfService < loanProductViewModel.GuarantorMinYear)
                return $"Guarantor's year of service ({guarantorVerifyModel.YearsOfService}) is less than the minimum ({loanProductViewModel.GuarantorMinYear}) for this loan";
            else
                return null;
        }
    }
}
