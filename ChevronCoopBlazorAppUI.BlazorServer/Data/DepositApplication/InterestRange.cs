namespace ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication
{
    public class RootObject
    {
        public ResponseObject response { get; set; }
    }

    public class ResponseObject
    {
        public List<InterestRange> interestRanges { get; set; }
    }
    public class InterestRange
    {
        public string ProductId { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }
        public double InterestRate { get; set; }
        public string Id { get; set; }
        public object Description { get; set; }
        public object Comments { get; set; }
        public bool IsActive { get; set; }
        public object CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public object UpdatedByUserId { get; set; }
        public DateTime DateUpdated { get; set; }
        public object DeletedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public object DateDeleted { get; set; }
        public string RowVersion { get; set; }
        public object FullText { get; set; }
        public object Tags { get; set; }
        public object Caption { get; set; }
    }
}
