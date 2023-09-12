using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public partial class DepartmentDepositProductPublicationViewModel : BaseViewModel
    {
        public string PublicationType { get; set; }
        public string ProductId { get; set; }
        public string DepartmentId { get; set; }

    }

}

