using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;

public class CreateApprovalGroupCommand : IRequest<CommandResult<ApprovalGroupViewModel>>
{
    public string Name { get; set; }

    [MaxLength(128)] public string CreatedByUserId { get; set; }

    public List<string> ApprovalGroupMemberIds { get; set; }
}