using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.Permissions
{
    public partial class PermissionViewModel : BaseViewModel
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
