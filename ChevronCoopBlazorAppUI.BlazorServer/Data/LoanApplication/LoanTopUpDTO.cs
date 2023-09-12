using AP.ChevronCoop.Entities;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanApplication
{
    public class LoanTopUpDTO
    {
        public string LoanProductId { get; set; }

        public string LoanApplicationId { get; set; }

        public string MemberProfileId { get; set; }

        public decimal Principal { get; set; }
        public string TenureUnit { get; set; }

        public decimal PrincipalBalance { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestBalance { get; set; }
        public decimal AdminFee { get; set; }

        public decimal TenureValue { get; set; }

        public bool UseSpecialDeposit { get; set; }

        public string? SpecialDepositId { get; set; }

        public string? DestinationAccountId { get; set; }
        public string CustomerBankAccountId { get; set; }
        public decimal TopUpAmount { get; set; }
        public DateTime TopupDate { get; set; }

        public DateTime CommencementDate { get; set; }

        public string ApprovalId { get; set; }
        public TopupFundingSourceType DestinationType { get; set; }
        public int ProvidedAccountId { get; set; }
        public string LoanAccountId { get; set; }
    }
}
