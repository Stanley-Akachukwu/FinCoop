using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Accounting.JournalEntries
{
    public class JournalEntry : BaseEntity<string>
    {

        public JournalEntry()
        {
            Id = NUlid.Ulid.NewUlid().ToString(); 
            Credit = 0;
            Debit = 0;
            DecimalPlaces = 3;
            EntryType = TransactionEntryType.DEBIT;
            IsPosted = false;
            IsReversed = false;
        }

        public string TransactionEntryNo { get; set; }

        public string TransactionJournalId { get; set; }

        //[ForeignKey(nameof(TransactionJournalId))]
        public virtual TransactionJournal TransactionJournal { get; set; }

        public virtual LedgerAccount Account { get; set; }

        public string AccountId { get; set; }

        public TransactionEntryType EntryType { get; set; }
        
        public int DecimalPlaces { get; set; }

        public decimal Debit { get; set; } = 0;

        public decimal Credit { get; set; } = 0;

        public DateTime TransactionDate { get; set; }

        public bool IsPosted { get; set; }
        public string PostedByUserName { get; set; }

        public DateTime? DatePosted { get; set; }



        public bool IsReversed { get; set; }
        public string ReversedByUserName { get; set; }
        public DateTime? ReversalDate { get; set; }


        public string Memo { get; set; }


        public override string DisplayCaption
        {
            get
            {
                return "";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }

    }
}