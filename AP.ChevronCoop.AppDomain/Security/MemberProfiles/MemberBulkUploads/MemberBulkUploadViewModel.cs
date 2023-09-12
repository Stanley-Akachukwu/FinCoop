namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads
{
    public class MemberBulkUploadViewModel
    {
        public List<MemberDataUpload> AcceptedMemberDataUpload { get; set; }
        public List<MemberDataUpload> RejectedMemberDataUpload { get; set; }
        public string SessionId { get; set; }
    }
}







