using AP.ChevronCoop.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.RetireeSwitches
{
    public partial class DeleteRetireeSwitchCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }
}
