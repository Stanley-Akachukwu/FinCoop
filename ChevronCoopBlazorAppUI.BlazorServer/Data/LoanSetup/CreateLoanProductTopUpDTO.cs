namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup
{
    public class CreateLoanProductTopUpDTO
    {
        public bool EnableTopUp { get; set; }

        public bool EnableTopUpCharges { get; set; }
        public List<string> TopUpCharges { get; set; }
    }
}
