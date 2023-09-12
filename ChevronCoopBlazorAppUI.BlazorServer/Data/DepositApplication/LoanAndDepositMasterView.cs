namespace ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication
{
    public class LoanAndDepositRequestMasterView
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public DateTime DateCreated { get; set; }

        public string IsLoanOrProduct { get; set; }
       
        public string ActionType { get; set; }

    }
}
