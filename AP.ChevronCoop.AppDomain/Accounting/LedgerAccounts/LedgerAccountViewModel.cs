using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts
{

    public partial class LedgerAccountViewModel : BaseViewModel
    {

        [MaxLength(32)]
        [Required]
        public string AccountType { get; set; }

        [MaxLength(32)]
        [Required]
        public string UOM { get; set; }
        [MaxLength(80)]
        public string? CurrencyId { get; set; }

        [MaxLength(64)]
        [Required]
        public string Code { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }
        [MaxLength(900)]
        public string? ParentId { get; set; }


        [Required]
        public decimal ClearedBalance { get; set; }


        [Required]
        public decimal UnclearedBalance { get; set; }


        [Required]
        public decimal LedgerBalance { get; set; }


        [Required]
        public decimal AvailableBalance { get; set; }


        [Required]
        public bool IsOfficeAccount { get; set; }


        [Required]
        public bool AllowManualEntry { get; set; }


        [Required]
        public bool IsClosed { get; set; }

        public DateTime? DateClosed { get; set; }

        public string? ClosedByUserName { get; set; }

    }




}
