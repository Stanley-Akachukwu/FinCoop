namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class LoanDisbursementDTO
    {
        public string LoanAccountId { get; set; }

        public string Amount { get; set; }

        public string? DisbursementAccountId { get; set; }

        public DateTimeOffset? DisbursementDate { get; set; }

        public string DisbursementMode { get; set; }

        public string? SpecialDepositAccountId { get; set; }

        public string? CustomerBankAccountId { get; set; }

        public string? CustomerDisburmentAccount_Name { get; set; }
        public string? CustomerDisburmentAccount_Type { get; set; }
    }
}
