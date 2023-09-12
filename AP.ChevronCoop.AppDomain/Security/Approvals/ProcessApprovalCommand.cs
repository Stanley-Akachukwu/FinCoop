using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;


namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public partial class ProcessApprovalCommand :  IRequest<CommandResult<string>>
    {
        public string ApprovalId { get; set; }
        public string? ApprovalWorkflowId { get; set; }
        public EntityDbTableLocation EntityDbTableLocation { get; set; }
        public List<ApprovalGroupMember> ApprovalGroupMembers { get; set; } = new List<ApprovalGroupMember>();
    }
}
