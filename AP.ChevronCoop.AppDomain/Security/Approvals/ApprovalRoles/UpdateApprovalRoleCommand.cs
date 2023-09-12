using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalRoles
{
    public partial class UpdateApprovalRoleCommand : UpdateCommand, IRequest<CommandResult<ApprovalRoleViewModel>>
    {

        [MaxLength(80)]
        [Required]
        public string EventGlobalCodeId { get; set; }

        [MaxLength(900)]
        [Required]
        public string RoleId { get; set; }

        [Required]
        public int Order { get; set; }

    }






}
