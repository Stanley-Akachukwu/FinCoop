namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup
{
    public class CreateLoanProductOffSetInfoDTO
    {
        public string AllowedOffsetType { get; set; }

        public string OffsetPeriodUnit { get; set; }

        public decimal OffsetPeriodValue { get; set; }

        public bool EnableSavingsOffset { get; set; }

        public bool EnableChargeWaiver { get; set; }
        public List<string> WaivedCharges { get; set; }

        public List<string> SavingsOffSets { get; set; }
        public bool EnableOffSetAdminCharges { get; set; }
        public List<string> OffSetsAdminCharges { get; set; }
    }
}
