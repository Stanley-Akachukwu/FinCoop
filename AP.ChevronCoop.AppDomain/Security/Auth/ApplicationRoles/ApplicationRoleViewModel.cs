using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles
{
    public partial class ApplicationRoleViewModel : BaseViewModel
    {


        [Required]
        public bool IsSystemRole { get; set; }
        [MaxLength(512)]
        public string Name { get; set; }
        [MaxLength(512)]
        public string? NormalizedName { get; set; }

        public string? ConcurrencyStamp { get; set; }
        
        public string Code { get; set; }
    }



}
