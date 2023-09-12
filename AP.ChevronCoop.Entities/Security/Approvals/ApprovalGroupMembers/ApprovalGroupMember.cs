using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers
{

    public class ApprovalGroupMember : BaseEntity<string>
    {
        public ApprovalGroupMember()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApprovalGroupId { get; set; }
        public virtual ApprovalGroup ApprovalGroup { get; set; }
        
        
        public int ApprovalSequence { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public override string DisplayCaption
        {
            get
            {
                return "";
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
