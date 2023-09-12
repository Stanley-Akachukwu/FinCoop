namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors
{
    public class LoanTopupGuarantorApprovalViewModel : BaseViewModel
    {
        public string LoanAccountId { get; set; }
        public string GuarantorId { get; set; }
        public string GuarantorType { get; set; }
        public decimal Amount { get; set; }
    }
}
