using AP.ChevronCoop.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads
{
    public class MemberDataUpload
    {
        public int RecordId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MembershipNumber { get; set; }
        public string MemberType { get; set; }
        public string Status { get; set; }
        public bool IsValid { get; set; }
        public List<ValidationMessage> Messages { get; set; }
    }

}
