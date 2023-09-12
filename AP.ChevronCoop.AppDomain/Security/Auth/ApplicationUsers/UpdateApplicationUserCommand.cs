using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers
{
    public partial class UpdateApplicationUserCommand : UpdateCommand, IRequest<CommandResult<ApplicationUserViewModel>>
    {


        // public string? AdObjectId { get; set; }
        //
        //
        // public string? SecondaryPhone { get; set; }
        //
        // // [Required]
        // public bool SecondaryPhoneConfirmed { get; set; }
        //
        // [MaxLength(512)]
        //
        // public string? UserName { get; set; }
        //
        // [MaxLength(512)]
        //
        // public string? NormalizedUserName { get; set; }
        //
        // [MaxLength(512)]
        //
        // public string? Email { get; set; }
        //
        // [MaxLength(512)]
        //
        // public string? NormalizedEmail { get; set; }
        //
        // // [Required]
        // public bool EmailConfirmed { get; set; }
        //
        //
        // public string? PasswordHash { get; set; }
        //
        //
        // public string? SecurityStamp { get; set; }
        //
        //
        // public string? ConcurrencyStamp { get; set; }
        //
        //
        // public string? PhoneNumber { get; set; }
        //
        // // [Required]
        // public bool PhoneNumberConfirmed { get; set; }
        //
        // // [Required]
        // public bool TwoFactorEnabled { get; set; }
        //
        //
        // public DateTimeOffset? LockoutEnd { get; set; }
        //
        // // [Required]
        // public bool LockoutEnabled { get; set; }
        //
        // // [Required]
        // public int AccessFailedCount { get; set; }
        // [MaxLength(40)]
        // [Required]
        // public string RoleId { get; set; }
        //
        // public UpdateMemberProfileCommand MemberProfile { get; set; }
        
        
        [MaxLength(512)]
        public string Email { get; set; }
        
        public string ApplicationUserId { get; set; }

        public string PhoneNumber { get; set; }
        
        [MaxLength(256)]
        public string FirstName { get; set; }

        [MaxLength(256)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string MiddleName { get; set; }
        public string MembershipId { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string DepartmentId { get; set; }
    }






}
