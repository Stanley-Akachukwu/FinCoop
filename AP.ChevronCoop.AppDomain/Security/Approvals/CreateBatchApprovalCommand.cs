using AP.ChevronCoop.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public partial class CreateBatchApprovalCommand : CreateCommand, IRequest<CommandResult<string>>
    {
        public List<CreateApprovalCommand> CreateApprovalCommands { get; set; }
    }
}
