using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace AP.ChevronCoop.Entities.Deposits.CommonViews
{
    [Table(nameof(SavingsActionsMasterView), Schema = "Deposits")]
    public partial class SavingsActionsMasterView
    {
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string SavingsAccountId { get; set; }
        public string SavingsAccountId_AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string CustomerID { get; set; }
        public string TransactionType { get; set; }
        public string ApprovalId_Status { get; set; }
        public string ApprovalId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? TransactionDate { get; set; } = DateTime.Now;
    }
}
 

