using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.JournalEntries
{
    public partial class JournalEntryViewModel : BaseViewModel
    {

        [MaxLength(64)]
        [Required]
        public string TransactionEntryNo { get; set; }

        [MaxLength(900)]
        [Required]
        public string TransactionJournalId { get; set; }

        [MaxLength(900)]
        [Required]
        public string AccountId { get; set; }

        [MaxLength(32)]
        [Required]
        public string EntryType { get; set; }


        [Required]
        public int DecimalPlaces { get; set; }


        [Required]
        public decimal Debit { get; set; }


        [Required]
        public decimal Credit { get; set; }


        [Required]
        public DateTime TransactionDate { get; set; }


        [Required]
        public bool IsPosted { get; set; }

        public string PostedByUserName { get; set; }

        public DateTime? DatePosted { get; set; }


        [Required]
        public bool IsReversed { get; set; }

        public string ReversedByUserName { get; set; }

        public DateTime? ReversalDate { get; set; }
        [MaxLength(1024)]
        public string Memo { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
