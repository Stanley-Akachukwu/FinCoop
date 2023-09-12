using AP.ChevronCoop.Commons;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads
{
     
    public class MemberBulkUploadSession : BaseEntity<string>
    {
        public MemberBulkUploadSession() 
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        public string ApprovedByUserId { get; set; }
        public int Size { get; set; }
        public string Status { get; set; }
        public string SessionId { get; set; }

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
