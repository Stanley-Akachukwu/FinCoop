

namespace AP.ChevronCoop.Entities.Deposits.DepositApplications
{
    public partial class DepositApplicationsMasterView  
    {
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string ApplicationNo { get; set; }
        public string DepositProductId { get; set; }
        public string ProductId_Name { get; set; }
        public string AccountType { get; set; }
        public string ApprovalId_Status { get; set; }
        public string ApprovalId { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public string? DeletedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
        public Guid RowVersion { get; set; }
        public string? FullText { get; set; }
        public string? Tags { get; set; }
        public string? Caption { get; set; }
    }
}


