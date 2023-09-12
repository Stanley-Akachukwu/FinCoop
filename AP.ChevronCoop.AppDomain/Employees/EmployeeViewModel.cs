using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Employees
{
    public partial class EmployeeViewModel : BaseViewModel
    {

        [MaxLength(100)]
        [Required]
        public string EmployeeNo { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FirstName { get; set; }

        public DateTime? Dob { get; set; }

        [MaxLength(64)]
        [Required]
        public string Gender { get; set; }

        public string ProfileImageUrl { get; set; }

        public DateTime? EmploymentDate { get; set; }
        [MaxLength(80)]
        public string DepartmentId { get; set; }
        [MaxLength(80)]
        public string ProfileId { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
