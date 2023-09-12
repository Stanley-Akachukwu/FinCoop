using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.TransactionDocuments;
using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Accounting.TransactionJournals
{
    public class TransactionJournal : BaseEntity<string>
    {

        public TransactionJournal()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
            TransactionDate = DateTime.Now;
            IsPosted = false;
            IsReversed = false;
            JournalEntries = new List<JournalEntry>();
        }

        public string TransactionNo { get; set; }

        public string Title { get; set; }

        public TransactionType TransactionType { get; set; }
        public string? DocumentRef { get; set; }

        public string? DocumentRefId { get; set; }


        public string? PostingRef { get; set; }

        public string? PostingRefId { get; set; }

        public string? EntityRef { get; set; }

        public string? EntityRefId { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool IsPosted { get; set; }
        public string? PostedByUserId { get; set; }
        public virtual ApplicationUser? PostedByUser { get; set; }
        public DateTime? DatePosted { get; set; }


        public bool IsReversed { get; set; }
        public string? ReversedByUserId { get; set; }
        public virtual ApplicationUser? ReversedByUser { get; set; }
        public DateTime? ReversalDate { get; set; }


        public string? ApprovalId { get; set; }
        public virtual Approval? Approval { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public string Memo { get; set; }


        [NotMapped]
        public decimal AllDebits
        {
            get
            {
                decimal? debits = 0;

                if (JournalEntries == null || !JournalEntries.Any())
                {
                    return debits ?? 0;
                }

                var data = JournalEntries.Where(r => r.EntryType == TransactionEntryType.DEBIT);

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

                if (JournalEntries == null || !JournalEntries.Any())
                {
                    return credits ?? 0;
                }

                var data = JournalEntries.Where(r => r.EntryType == TransactionEntryType.CREDIT);
                credits = data.Select(r => r.Credit).Sum();

                return credits ?? 0;
            }

        }

        [NotMapped]
        public bool IsBalanced
        {
            get
            {
                if (JournalEntries == null || !JournalEntries.Any())
                {
                    return false;
                }

                return AllCredits == AllDebits;
            }

        }


        public virtual List<JournalEntry> JournalEntries { get; set; }


        public virtual List<TransactionDocument> TransactionDocuments { get; set; }


        public void Post(/*ChevronCoopDbContext dbContext = null*/)
        {

            try
            {

                //var _entries = JournalEntries.Select(p => p.AccountId);
                //var emptyAccountIds = _entries.Where(p => string.IsNullOrEmpty(p)).Any();
                //if (emptyAccountIds)
                //{
                //    throw new InvalidOperationException($"Transaction entry AccountId cannot be empty or null");
                //}



                if (JournalEntries == null || !JournalEntries.Any())
                {
                    return;
                }


                if (!IsBalanced)
                {
                    throw new InvalidOperationException($"Transaction entries must balance {AllDebits:n2} <> {AllCredits:n2}");
                }


                if (AllDebits == 0)
                {
                    throw new InvalidOperationException($"Debits ({AllDebits:n2}) cannot be equal to zero");
                }

                if (AllCredits == 0)
                {
                    throw new InvalidOperationException($"Debits ({AllCredits:n2}) cannot be equal to zero");
                }

                if (IsPosted)
                {
                    throw new InvalidOperationException($"Transaction entries have already been posted");
                }


                foreach (var entry in JournalEntries)
                {
                    var account = entry.Account;

                    if (account.IsClosed || !account.IsActive)
                    {
                        throw new InvalidOperationException("Transaction entry account cannot be in-active or closed");
                    }

                    if (account.LedgerBalance <= 0)
                        account.LedgerBalance = 0;

                    var LedgerBalance = account.LedgerBalance;

                    if (entry.EntryType == TransactionEntryType.DEBIT)
                    {
                        account.LedgerBalance = LedgerBalance - entry.Debit;
                    }
                    else
                    {
                        account.LedgerBalance = LedgerBalance + entry.Credit;
                    }

                    entry.IsPosted = true;
                    entry.DatePosted = DateTime.Now;
                    //entry.PostedBy = GetCurrentUser();
                    entry.IsReversed = false;
                }

                IsPosted = true;
                //PostedBy = GetCurrentUser();
                DatePosted = DateTime.Now;
                IsReversed = false;

                //objectSpace.CommitChanges();
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                // throw;
            }
        }

        public void Reverse()
        {

            if (JournalEntries == null || !JournalEntries.Any())
            {
                return;
            }


            if (!IsBalanced)
            {
                throw new InvalidOperationException("Transaction entries must balance");
            }

            if (IsReversed)
            {
                throw new InvalidOperationException($"Transaction entries have already been reversed");
            }


            foreach (var entry in JournalEntries)
            {
                var account = entry.Account;

                if (account.IsClosed || !account.IsActive)
                {
                    throw new InvalidOperationException("Transaction entry account cannot be in-active or closed");
                }

                if (account.LedgerBalance <= 0)
                    account.LedgerBalance = 0;

                var LedgerBalance = account.LedgerBalance;

                if (entry.EntryType == TransactionEntryType.DEBIT)
                {
                    account.LedgerBalance = LedgerBalance + entry.Debit;
                }
                else
                {
                    account.LedgerBalance = LedgerBalance - entry.Credit;
                }

                entry.IsPosted = false;
                entry.IsReversed = true;
                //entry.ReversedBy = GetCurrentUser();
                entry.ReversalDate = DateTime.Now;
            }

            IsPosted = false;
            IsReversed = true;
            //ReversedBy = GetCurrentUser();
            ReversalDate = DateTime.Now;

            //objectSpace.CommitChanges();
        }



        public override string DisplayCaption
        {
            get
            {
                return $"{TransactionNo}";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return $"{TransactionNo}";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return $"{TransactionNo}";
            }
        }

    }
}