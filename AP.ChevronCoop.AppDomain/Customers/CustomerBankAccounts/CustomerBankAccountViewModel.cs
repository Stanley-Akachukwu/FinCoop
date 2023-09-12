using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts
{
    public partial class CustomerBankAccountViewModel : BaseViewModel
    {

        public string ProfileId { get; set; }
        [MaxLength(80)]
        public string MemberProfileId { get; set; }
        [MaxLength(80)]
        public string BankId { get; set; }

        [MaxLength(64)]
        public string BVN { get; set; }

        [MaxLength(100)]
        [Required]
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }

        [MaxLength(256)]
        [Required]
        public string AccountName { get; set; }
        public string Branch { get; set; }

    }



}
