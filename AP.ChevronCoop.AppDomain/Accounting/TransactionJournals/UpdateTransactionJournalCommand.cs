using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.AppDomain.Accounting.TransactionJournals
{
    public partial class UpdateTransactionJournalCommand : UpdateCommand, IRequest<CommandResult<TransactionJournalViewModel>>
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


        public bool PostEntries { get; set; }


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


        public List<UpdateJournalEntryCommand> OldJournalEntries { get; set; }
        public List<string> OldJournalEntryIds { get; set; }
        public List<string> DeletedJournalEntryIds { get; set; }
        public List<UpdateJournalEntryCommand> NewJournalEntries { get; set; }


        [NotMapped]
        public decimal AllDebits
        {
            get
            {
                decimal? debits = 0;

                if (NewJournalEntries == null || !NewJournalEntries.Any())
                {
                    return debits ?? 0;
                }

                var data = NewJournalEntries.Where(r => r.EntryType == TransactionEntryType.DEBIT.ToString());

                debits = data.Select(r => r.Debit).Sum();

                return debits ?? 0;
            }

        }

        [NotMapped]
        public decimal AllCredits
        {

            get
            {
                decimal? credits = 0;

                if (NewJournalEntries == null || !NewJournalEntries.Any())
                {
                    return credits ?? 0;
                }

                var data = NewJournalEntries.Where(r => r.EntryType == TransactionEntryType.CREDIT.ToString());
                credits = data.Select(r => r.Credit).Sum();

                return credits ?? 0;
            }

        }

        [NotMapped]
        public bool IsBalanced
        {
            get
            {
                if (NewJournalEntries == null || !NewJournalEntries.Any())
                {
                    return false;
                }

                return AllCredits == AllDebits;
            }

        }

    }







}
