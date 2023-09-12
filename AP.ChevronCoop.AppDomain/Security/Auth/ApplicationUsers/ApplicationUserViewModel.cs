using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers
{
    public partial class ApplicationUserViewModel : BaseViewModel
    {

        public string? AdObjectId { get; set; }

        public string? SecondaryPhone { get; set; }


        [Required]
        public bool SecondaryPhoneConfirmed { get; set; }
        [MaxLength(512)]
        public string? UserName { get; set; }
        [MaxLength(512)]
        public string? NormalizedUserName { get; set; }
        [MaxLength(512)]
        public string? Email { get; set; }
        [MaxLength(512)]
        public string? NormalizedEmail { get; set; }


        [Required]
        public bool EmailConfirmed { get; set; }

        public string? PasswordHash { get; set; }

        public string? SecurityStamp { get; set; }

        public string? ConcurrencyStamp { get; set; }

        public string? PhoneNumber { get; set; }


        [Required]
        public bool PhoneNumberConfirmed { get; set; }


        [Required]
        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }


        [Required]
        public bool LockoutEnabled { get; set; }


        [Required]
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {MiddleName} {LastName}"; }  }

    }



}
