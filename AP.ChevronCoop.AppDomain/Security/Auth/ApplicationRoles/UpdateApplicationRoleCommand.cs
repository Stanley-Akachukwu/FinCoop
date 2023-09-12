using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles
{
    public partial class UpdateApplicationRoleCommand : UpdateCommand, IRequest<CommandResult<ApplicationRoleViewModel>>
    {

        // [Required]
        public bool IsSystemRole { get; set; }

        [MaxLength(512)]

        public string? Name { get; set; }

        [MaxLength(512)]

        public string? NormalizedName { get; set; }


        public string? ConcurrencyStamp { get; set; }
        
        public List<string> PermissionIds { get; set; }
        
        public string? Code { get; set; }
    }
}
