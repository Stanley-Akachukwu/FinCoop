using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Deposits.CommonViews
{
   
    [Table(nameof(FixedDepositActionsMasterView), Schema = "Deposits")]
    public partial class FixedDepositActionsMasterView
    {
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string FixedDepositAccountId { get; set; }
        public string FixedDepositAccountId_AccountNo { get; set; }
        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
        public string CustomerID { get; set; }
        public string TransactionType { get; set; }
        public string ApprovalId_Status { get; set; }
        public DateTimeOffset? TransactionDate { get; set; } = DateTime.Now;
    }
}

