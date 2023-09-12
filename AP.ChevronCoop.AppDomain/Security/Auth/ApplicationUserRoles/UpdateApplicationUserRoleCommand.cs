using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles
{
    public partial class UpdateApplicationUserRoleCommand : IRequest<CommandResult<List<ApplicationUserRoleViewModel>>>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public List<string> RoleId { get; set; }

    }






}
