using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.CompanyBankAccounts
{
    public partial class CompanyBankAccountViewModel : BaseViewModel
    {

        [MaxLength(80)]
        [Required]
        public string LedgerAccountId { get; set; }

        [MaxLength(80)]
        [Required]
        public string BankId { get; set; }
        [MaxLength(256)]
        public string BranchName { get; set; }
        [MaxLength(256)]
        public string BranchAddress { get; set; }

        [MaxLength(80)]
        [Required]
        public string CurrencyId { get; set; }

        [MaxLength(256)]
        [Required]
        public string AccountName { get; set; }

        [MaxLength(64)]
        [Required]
        public string AccountNumber { get; set; }
        [MaxLength(64)]
        public string BVN { get; set; }


        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
