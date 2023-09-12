using AP.ChevronCoop.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads
{
    public class ApproveMemberBulkUploadCommand : IRequest<CommandResult<MemberBulkUploadViewModel>>
    {
        public string MemberBulkUploadSessionId { get; set; }
        public string ApprovalId { get; set; }
        public string ApprovedById { get; set; }

    }
}
