using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionJournals
{
    public partial class TransactionJournalViewModel : BaseViewModel
    {

        [MaxLength(64)]
        [Required]
        public string TransactionNo { get; set; }

        [MaxLength(512)]
        [Required]
        public string Title { get; set; }
     
        public string? TransactionType { get; set; }
        public string? DocumentRef { get; set; }

        public string? DocumentRefId { get; set; }


        public string? PostingRef { get; set; }

        public string? PostingRefId { get; set; }

        public string? EntityRef { get; set; }

        public string? EntityRefId { get; set; }


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
