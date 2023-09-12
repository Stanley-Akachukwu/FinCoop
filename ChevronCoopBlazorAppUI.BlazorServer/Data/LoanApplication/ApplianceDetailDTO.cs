using AP.ChevronCoop.Entities;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanApplication
{
    public class ApplianceDetailDTO
    {
        public LoanApplicationItemType ItemType { get; set; }

        public string Name { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public decimal Amount { get; set; }
    }
}
