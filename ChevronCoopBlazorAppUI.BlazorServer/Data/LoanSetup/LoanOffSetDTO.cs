using AP.ChevronCoop.Entities;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup
{
    public class LoanOffSetDTO
    {
        public string LoanAccountId { get; set; }

        public decimal OffsetAmount { get; set; }

        public decimal PrincipalBalance { get; set; }

        public decimal LedgerBalance { get; set; }

        public decimal InterestBalance { get; set; }

        public decimal TotalOffsetCharges { get; set; }

        public string AllowedOffsetType { get; set; }
        public string RepaymentScheduleId { get; set; }

        public LoanRepaymentMode LoanRepaymentMode { get; set; }

        public DateTimeOffset OffSetRepaymentDate { get; set; }

        public string SavingsAccountId { get; set; }

        public string SpecialDepositAccountId { get; set; }

        public DateTime DeductionStartAfter { get; set; }

        public DateTime OffsetToBeCalculatedAfter { get; set; }

        public string CustomerPaymentDocumentId { get; set; }

        public List<string> AdminCharges { get; set; }

        public decimal BankTransferAmount { get; set; }
        public bool UseSpecialDeposit { get; set; }

        public string? SpecialDepositId { get; set; }

        public string? DestinationAccountId { get; set; }
    }
}
