using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads
{
    public class ValidateMemberBulkUploadCommand : IRequest<CommandResult<MemberBulkUploadViewModel>>
    {
        public List<MemberDataUpload> MemberDataUploads { get; set; }
        public string UploadedByUserId { get; set; }
        public string ApprovalWorkflowId { get; set; }
        public string SessionId { get; set; }
    }
}








