using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.Security.MemberProfiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.CoopCustomers;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;

namespace AP.ChevronCoop.Entities.Employees
{
    public class Employee : BaseEntity<string>
    {

        public Employee()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
            EmployeeNo = NUlid.Ulid.NewUlid().ToString();
        }


        public string EmployeeNo { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public DateTime? Dob { get; set; }
        public Gender Gender { get; set; }
        public string ProfileImageUrl { get; set; }


        public DateTime? EmploymentDate { get; set; }


        public virtual Department Department { get; set; }
        
        public string CustomerId { get; set; }
        
        public virtual Customer Customer { get; set; }

        //public string HomeAddress { get; set; }
        //public string OfficeAddress { get; set; }
        //public string PrimaryPhone { get; set; }
        //public string SecondaryPhone { get; set; }
        //public string PrimaryEmail { get; set; }
        //public string SecondaryEmail { get; set; }

        public override string DisplayCaption
        {
            get
            {
                return $"";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }
    }
}
