namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup
{
    public class CreateLoanProductWhenDueDTO
    {
        public bool EnableWaitingPeriod { get; set; }

        public string WaitingPeriodUnit { get; set; }

        public decimal WaitingPeriodValue { get; set; }

        public bool EnableWaitingPeriodCharge { get; set; }
        public List<string> WaitingPeriodCharges { get; set; }
    }
}
