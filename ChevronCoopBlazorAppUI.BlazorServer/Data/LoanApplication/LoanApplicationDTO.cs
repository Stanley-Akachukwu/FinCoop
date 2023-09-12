using AP.ChevronCoop.Entities;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanApplication
{
    public class LoanApplicationDTO
    {
        public string LoanProductId { get; set; }

        public string MemberProfileId { get; set; }

        public decimal Amount { get; set; }

        public Tenure RepaymentTenureUnit { get; set; }

        public int RepaymentPeriod { get; set; }

        public DateTime RepaymentCommencementDate { get; set; }

        public bool UseSpecialDeposit { get; set; }

        public string? SpecialDepositId { get; set; }

        public string? DestinationAccountId { get; set; }
    }
}
