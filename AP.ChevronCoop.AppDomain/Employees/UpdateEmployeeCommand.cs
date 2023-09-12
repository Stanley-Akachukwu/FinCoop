using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Employees
{
    public partial class UpdateEmployeeCommand : UpdateCommand, IRequest<CommandResult<EmployeeViewModel>>
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

        
    }







}
