using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.CustomerDepositProductPublications
{
    public partial class CustomerDepositProductPublicationViewModel : BaseViewModel
    {
        public string PublicationType { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
    }

}

