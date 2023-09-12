using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.RetireeSwitches
{
    public partial class ApproveRetireeSwitchCommand : IRequest<CommandResult<RetireeSwitchViewModel>>
    {
        public string MemberProfileId { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovalWorkflowId { get; set; }
        public string ApprovalId { get; set; }
        public string Comment { get; set; }
        public string RetireeSwitchEntityId { get; set; }
    }
}
