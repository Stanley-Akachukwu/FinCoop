using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Deposits.CommonViews
{
    [Table(nameof(SpecialDepositActionsMasterView), Schema = "Deposits")]
    public partial class SpecialDepositActionsMasterView
    {
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string SpecialDepositAccountId { get; set; }
        public string SpecialDepositAccountId_AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string CustomerID { get; set; }
        public string TransactionType { get; set; }
        public string ApprovalId_Status { get; set; }
        public string ApprovalId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? TransactionDate { get; set; } = DateTime.Now;
    }
}


