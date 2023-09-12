using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs
{
    public class ApprovalLog : BaseEntity<string>
    {

        public ApprovalLog()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApprovalId { get; set; }
        public Approval Approval { get; set; }
        public int Sequence { get; set; }
        
        public string? ApprovalGroupId { get; set; }
        public ApprovalGroup ApprovalGroup { get; set; }
        public string ApprovedByUserId { get; set; }
        public ApplicationUser ApprovedByUser { get; set; }
        public DateTime DateApproved { get; set; }
        public ApprovalStatus Status { get; set; }
        public string? Comment { get; set; }
        

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
