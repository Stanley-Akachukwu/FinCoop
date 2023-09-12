namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
   
        public class UpdateBiodataItem
        {
            public string id { get; set; }
            public string description { get; set; }
            public string comments { get; set; }
            public bool isActive { get; set; }
            public string createdByUserId { get; set; }
            public DateTime dateCreated { get; set; }
            public string updatedByUserId { get; set; }
            public DateTime dateUpdated { get; set; }
            public string deletedByUserId { get; set; }
            public bool isDeleted { get; set; }
            public DateTime? dateDeleted { get; set; }
            public string rowVersion { get; set; }
            public string fullText { get; set; }
            public string tags { get; set; }
            public string caption { get; set; }
            public string lastName { get; set; }
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string gender { get; set; }
            public string primaryEmail { get; set; }
            public string secondaryEmail { get; set; }
            public string primaryPhone { get; set; }
            public string secondaryPhone { get; set; }
            public string membershipId { get; set; }
            public string cai { get; set; }
            public string retireeNumber { get; set; }
            public string residentialAddress { get; set; }
            public string officeAddress { get; set; }
            public string rank { get; set; }
            public string departmentId { get; set; }
            public string jobRole { get; set; }
            public string applicationUserId { get; set; }
            public string status { get; set; }
            public string profileImageUrl { get; set; }

            public string passportUrl { get; set; }
            public string country { get; set; }
            public string state { get; set; }
            public string stateOfOrigin { get; set; }
            public bool isKycStarted { get; set; }
            public bool isKycCompleted { get; set; }
            public DateTime? kycStartDate { get; set; }
            public DateTime? kycCompletedDate { get; set; }
        }
    
}
