
namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public class UserApprovalViewModel
    {
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string? ApprovalViewModelPayload { get; set; }
        public string? ApprovalType { get; set; }
        public string Description { get; set; }
        public string? Status { get; set; }
        public DateTimeOffset? DateCreated { get; set; }

    }
}
