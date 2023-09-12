using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.Permissions
{
    public partial class UpdatePermissionCommand : UpdateCommand, IRequest<CommandResult<PermissionViewModel>>
    {

        [MaxLength(256)]
        [Required]
        public string Code { get; set; }

        [MaxLength(512)]
        [Required]
        public string Name { get; set; }

        [MaxLength(512)]

        public string? Group { get; set; }

        [MaxLength(512)]

        public string? Category { get; set; }

        [MaxLength(512)]

        public string? Module { get; set; }

        [MaxLength(512)]

        public string? Owner { get; set; }

    }






}
