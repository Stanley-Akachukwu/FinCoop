using AP.ChevronCoop.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads
{
    public class CreateMemberBulkUploadCommand : IRequest<CommandResult<MemberBulkUploadViewModel>>
    {
        public List<MemberDataUpload> MemberDataUploads { get; set; }
        public string UploadedByUserId { get; set; } 
        public string ApprovalWorkflowId { get; set; }
        public string SessionId { get; set; }

    }
}








