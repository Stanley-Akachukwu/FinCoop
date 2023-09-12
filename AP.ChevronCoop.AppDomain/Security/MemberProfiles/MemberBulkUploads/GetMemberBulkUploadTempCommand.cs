using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads
{
    
    public class GetMemberBulkUploadTempCommand : IRequest<CommandResult<List<MemberBulkUploadTemp>>>
    {
        public string SessionId { get; set; }

    }
}
