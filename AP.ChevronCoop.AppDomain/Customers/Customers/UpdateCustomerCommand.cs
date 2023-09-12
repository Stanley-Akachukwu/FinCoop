using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Customers.Customers
{
    public partial class UpdateCustomerCommand : UpdateCommand, IRequest<CommandResult<CustomerViewModel>>
    {

        [MaxLength(100)]
        [Required]
        public string CustomerNo { get; set; }


        public string LastName { get; set; }


        public string MiddleName { get; set; }


        public string FirstName { get; set; }


        public DateTime? Dob { get; set; }

        [MaxLength(64)]
        [Required]
        public string Gender { get; set; }


        public string ProfileImageUrl { get; set; }


        public DateTime? RegistrationDate { get; set; }

        [MaxLength(40)]

        public string DepartmentId { get; set; }

        [MaxLength(40)]

        public string ProfileId { get; set; }

       
    }







}
