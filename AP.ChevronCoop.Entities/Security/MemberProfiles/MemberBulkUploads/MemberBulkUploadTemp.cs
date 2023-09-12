using AP.ChevronCoop.Commons;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads
{
    public class MemberBulkUploadTemp : BaseEntity<string>
    {

        public MemberBulkUploadTemp()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public int RecordId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MembershipNumber { get; set; }
        public string UserRole { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public bool IsValid { get; set; }
        public string UploadedByUserId { get; set; }
        public string SessionId { get; set; }
        public string ApprovalId { get; set; }
        public string MemberType { get; set; }
        public bool IsSuccessfullyRegistered { get; set; }
        

        public override string DisplayCaption
        {
            get
            {
                return "";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }
    }
}
