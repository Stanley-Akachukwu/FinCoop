
namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles
{
    public partial class MemberProfileViaUploadMasterView
	{
        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsKycStarted { get; set; }
        public bool IsKycCompleted { get; set; }
        public DateTime? KycStartDate { get; set; }
        public DateTime? KycCompletedDate { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? PassportUrl { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? CAI { get; set; }
        public string? RetireeNumber { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? PrimaryEmail { get; set; }
        public string? SecondaryEmail { get; set; }
        public string? PrimaryPhone { get; set; }
        public string? SecondaryPhone { get; set; }
        public string? ResidentialAddress { get; set; }
        public string? OfficeAddress { get; set; }
        public string? Rank { get; set; }
        public string? JobRole { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
		public string MemberBulkUploadTempId_MembershipNumber { get; set; }
		public string MemberBulkUploadTempId_UserRole { get; set; } 
		public string MemberBulkUploadTempId_UploadedByUserId { get; set; }
		public string MemberBulkUploadSessionId_ApprovedByUserId { get; set; }
		public string MemberBulkUploadTempId_MemberBulkUploadSessionId { get; set; }
		public string MemberBulkUploadSessionId_ApprovedStatus { get; set; }

	}
}
