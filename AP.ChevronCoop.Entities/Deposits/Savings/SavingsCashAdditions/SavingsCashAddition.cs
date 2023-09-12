using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions
{
    public class SavingsCashAddition : BaseEntity<string>
    {
        public SavingsCashAddition()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }

        public string SavingsAccountId { get; set; }
        public SavingsAccount SavingsAccount { get; set; }
        public decimal Amount { get; set; }
        public DepositFundingSourceType ModeOfPayment { get; set; }
        public string? CustomerPaymentDocumentId { get; set; }
        public CustomerPaymentDocument? CustomerPaymentDocument { get; set; }

        //internal field to track payroll deductions for funding
        public string BatchRefNo { get; set; }
        public string? TransactionJournalId { get; set; }
        public virtual TransactionJournal TransactionJournal { get; set; }

        public string? ApprovalId { get; set; }
        public Approval? Approval { get; set; }


        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.PENDING;
        public override string DisplayCaption { get; }

        public override string DropdownCaption { get; }

        public override string ShortCaption { get; }
    }
}
