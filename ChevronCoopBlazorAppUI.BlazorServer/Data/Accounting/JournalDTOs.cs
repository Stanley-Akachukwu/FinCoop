using DocumentFormat.OpenXml.Spreadsheet;
using Syncfusion.Blazor.PivotView;
using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data.Accounting
{
    public class JournalEntryDTO
    {
        public JournalEntryDTO()
        {

        }
        public string TransactionEntryNo { get; set; } = "1";

        //public string TransactionJournalId { get; set; }

        //public string TransactionJournalNo { get; set; }

        //public string TransactionJournalTitle { get; set; }

        //[MaxLength(40)]
        //[Required]
        //public string AccountId { get; set; }

        public string AccountCode { get; set; }

        //public string AccountName { get; set; }

        [Required]
        public string EntryType { get; set; }

        //[Required]
        public int DecimalPlaces { get; set; }

        [Required]
        public decimal Debit { get; set; }

        [Required]
        public decimal Credit { get; set; }


        public DateTime Timestamp { get; set; } = DateTime.Now;

        //[Required]
        //public DateTime TransactionDate { get; set; }

        //[Required]
        //public bool IsPosted { get; set; }

        //public string PostedByUserName { get; set; }

        //public DateTime? DatePosted { get; set; }

        //[Required]
        //public bool IsReversed { get; set; }

        //public string ReversedByUserName { get; set; }

        //public DateTime? ReversalDate { get; set; }

        //[MaxLength(1024)]
        //public string Memo { get; set; }

        //public new string Description { get; set; }
    }




    public class JournalEntryItems : List<JournalEntryDTO>
    {
        public new void Add(JournalEntryDTO item)
        {
            //item.TransactionEntryNo = $"{Count + 1}";

            var checkEntryNoExists = this.Any(p => p.TransactionEntryNo == item.TransactionEntryNo);
            if (checkEntryNoExists)
            {
                throw new InvalidDataException($"Duplicate Entry No {item.TransactionEntryNo} already exists");
            }

            base.Add(item);
            //item.TransactionEntryNo = $"{IndexOf(item) + 1}";


            //System.Console.WriteLine($"index of new item->{IndexOf(item)}");
            //System.Console.WriteLine($"entry no of new item->{item.TransactionEntryNo}");
        }
        public new void Remove(JournalEntryDTO item)
        {
            base.Remove(item);
            item.TransactionEntryNo = $"-1";
            Reseed();
        }
        public new void RemoveAt(int index)
        {
            JournalEntryDTO m = this[index];
            this.Remove(m);
        }
        public new int RemoveAll(Predicate<JournalEntryDTO> match)
        {
            List<JournalEntryDTO> removing = new List<JournalEntryDTO>();
            foreach (JournalEntryDTO m in this)
                if (match(m))
                    removing.Add(m);

            foreach (JournalEntryDTO m in removing)
            {
                this.Remove(m);
            }
            return removing.Count;
        }
        public new void RemoveRange(int index, int count)
        {
            for (int i = index; i < count; i++)
            {
                JournalEntryDTO m = this[i];
                this.Remove(m);
            }
        }
        public new JournalEntryDTO this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
                Reseed();
            }
        }

        private void Reseed()
        {
            foreach (JournalEntryDTO item in this)
            {
                item.TransactionEntryNo = $"{IndexOf(item) + 1}";
            }
        }
    }
}
