using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.MemberApprovalActions
{
    public class MemberApprovalActionViewModel
    {
        public ApprovalGroupMember ApprovalGroupMember { get; set; }
        public string Comment { get; set; }
        public bool HasApproved { get; set; }
        public int ApprovalSequence { get; set; }
    }
}
