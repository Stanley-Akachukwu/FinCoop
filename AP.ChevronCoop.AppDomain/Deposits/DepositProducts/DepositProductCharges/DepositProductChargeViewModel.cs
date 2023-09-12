using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges
{
    public partial class DepositProductChargeViewModel : BaseViewModel
    {
        public string ProductId { get; set; }
        public string ChargeId { get; set; }

    }

}
