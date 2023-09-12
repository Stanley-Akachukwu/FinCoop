namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup
{
    public class CreateLoanProductGuarantorDTO
    {
        public bool IsGuarantorRequired { get; set; }

        public int GuarantorMinYear { get; set; }

        public decimal GuarantorAmountLimit { get; set; }

        public int EmployeeGuarantorCount { get; set; }

        public int NonEmployeeGuarantorCount { get; set; }
    }
}
