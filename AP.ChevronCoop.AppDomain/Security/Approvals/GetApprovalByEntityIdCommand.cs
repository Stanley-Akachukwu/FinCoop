using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace AP.ChevronCoop.AppDomain.Security.Approvals
{
    public partial class GetApprovalByEntityIdCommand : IRequest<CommandResult<ApprovalViewModel>>
    {
        [Required]
        public string EntityId { get; set; }
    }

}
