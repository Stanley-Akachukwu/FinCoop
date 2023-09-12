using AP.ChevronCoop.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows
{
    public partial class DeleteApprovalWorkflowCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
