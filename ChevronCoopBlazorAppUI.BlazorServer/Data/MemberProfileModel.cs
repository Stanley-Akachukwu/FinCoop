using AP.ChevronCoop.AppDomain;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class MemberProfileModel
    {

        public string applicationUserId { get; set; }
        public object applicationUser { get; set; }
        public bool isKycStarted { get; set; }
        public bool isKycCompleted { get; set; }
        public DateTime? kycStartDate { get; set; }
        public DateTime? kycCompletedDate { get; set; }
        public int status { get; set; }
        public int gender { get; set; }
        public object profileImageUrl { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string departmentId { get; set; }
        public object department { get; set; }
        public object membershipId { get; set; }
        public string cai { get; set; }
        public object retireeNumber { get; set; }
        public object stateOfOrigin { get; set; }
        public object primaryEmail { get; set; }
        public object secondaryEmail { get; set; }
        public object primaryPhone { get; set; }
        public object secondaryPhone { get; set; }
        public object residentialAddress { get; set; }
        public object officeAddress { get; set; }
        public object rank { get; set; }
        public object jobRole { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string displayCaption { get; set; }
        public string dropdownCaption { get; set; }
        public string shortCaption { get; set; }
        public string id { get; set; }
        public object description { get; set; }
        public bool isActive { get; set; }
        public object createdByUserId { get; set; }
        public DateTime dateCreated { get; set; }
        public object updatedByUserId { get; set; }
        public DateTime dateUpdated { get; set; }
        public object deletedByUserId { get; set; }
        public bool isDeleted { get; set; }
        public object dateDeleted { get; set; }
        public string rowVersion { get; set; }
        public object fullText { get; set; }
        public string tags { get; set; }
        public object caption { get; set; }

    }
}
