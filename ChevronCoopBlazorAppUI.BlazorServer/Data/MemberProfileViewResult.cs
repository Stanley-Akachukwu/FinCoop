using Newtonsoft.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class MemberProfileViewResult
    {
        
        public IEnumerable<MemberProfileViewModelResult> value { get; set; }
    }

    public class MemberProfileViewModelResult
    {
        //public int rowNumber { get; set; }
        //public string? id { get; set; }
        //public string? applicationUserId { get; set; }
        //public bool isKycStarted { get; set; }
        //public bool isKycCompleted { get; set; }
        //public DateTime? kycStartDate { get; set; }
        //public DateTime? kycCompletedDate { get; set; }
        //public string? status { get; set; }
        //public string? gender { get; set; }
        //public string? profileImageUrl { get; set; }
        //public string? firstName { get; set; }
        //public string? lastName { get; set; }
        //public string? middleName { get; set; }
        //public string? cai { get; set; }
        //public string? retireeNumber { get; set; }
        //public string? address { get; set; }
        //public string? country { get; set; }
        //public string? state { get; set; }

        ////changed
        //public string? primaryEmail { get; set; }
        ////changed
        //public string? secondaryEmail { get; set; }
        ////changed
        //public string? primaryPhone { get; set; }
        ////string
        //public string? secondaryPhone { get; set; }
        ////changed
        //public string? residentialAddress { get; set; }
        ////changed
        //public string? officeAddress { get; set; }
        //public string? rank { get; set; }
        //public string? jobRole { get; set; }
        //public string? stateOfOrigin { get; set; }
        ////changed
        //public string? membershipId { get; set; }
        //public string? departmentId { get; set; }
        //public string? description { get; set; }
        //public bool isActive { get; set; }
        //public string? createdByUserId { get; set; }
        //public DateTime dateCreated { get; set; }
        //public object? updatedByUserId { get; set; }
        //public DateTime? dateUpdated { get; set; }
        //public string? deletedByUserId { get; set; }
        //public bool? isDeleted { get; set; }
        //public DateTime? dateDeleted { get; set; }
        //public string? rowVersion { get; set; }
        //public string? fullText { get; set; }
        //public string? tags { get; set; }
        //public string? caption { get; set; }
        //public object applicationUserId_AdObjectId { get; set; }
        //public object applicationUserId_SecondaryPhone { get; set; }
        //public bool? applicationUserId_SecondaryPhoneConfirmed { get; set; }
        //public string? applicationUserId_UserName { get; set; }
        //public string? applicationUserId_Email { get; set; }
        //public bool? applicationUserId_EmailConfirmed { get; set; }
        //public string? applicationUserId_PhoneNumber { get; set; }
        //public bool? applicationUserId_PhoneNumberConfirmed { get; set; }
        //public bool? applicationUserId_TwoFactorEnabled { get; set; }
        //public object? applicationUserId_LockoutEnd { get; set; }
        //public bool? applicationUserId_LockoutEnabled { get; set; }
        //public int? applicationUserId_AccessFailedCount { get; set; }
        //public string? departmentId_Name { get; set; }
        //public bool? departmentId_IsActive { get; set; }
        //public string? departmentId_CreatedByUserId { get; set; }
        //public object departmentId_UpdatedByUserId { get; set; }
        //public object departmentId_DeletedByUserId { get; set; }
        //public bool? departmentId_IsDeleted { get; set; }
        //public string? departmentId_Tags { get; set; }
        //public string? departmentId_Caption { get; set; }
        //public string? fullName { get; set; }

        public int rowNumber { get; set; }
        public string id { get; set; }
        public string applicationUserId { get; set; }
        public bool isKycStarted { get; set; }
        public bool isKycCompleted { get; set; }
        public DateTime? kycStartDate { get; set; }
        public DateTime? kycCompletedDate { get; set; }
        public string status { get; set; }
        public string gender { get; set; }
        public string profileImageUrl { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string cai { get; set; }
        public string retireeNumber { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string primaryEmail { get; set; }
        public string secondaryEmail { get; set; }
        public string primaryPhone { get; set; }
        public string secondaryPhone { get; set; }
        public string residentialAddress { get; set; }
        public string officeAddress { get; set; }
        public string rank { get; set; }
        public string jobRole { get; set; }
        public string stateOfOrigin { get; set; }
        public string membershipId { get; set; }
        public string departmentId { get; set; }
        public string description { get; set; }
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
        public object applicationUserId_AdObjectId { get; set; }
        public object applicationUserId_SecondaryPhone { get; set; }
        public bool applicationUserId_SecondaryPhoneConfirmed { get; set; }
        public string applicationUserId_UserName { get; set; }
        public string applicationUserId_Email { get; set; }
        public bool applicationUserId_EmailConfirmed { get; set; }
        public string applicationUserId_PhoneNumber { get; set; }
        public bool applicationUserId_PhoneNumberConfirmed { get; set; }
        public bool applicationUserId_TwoFactorEnabled { get; set; }
        public object applicationUserId_LockoutEnd { get; set; }
        public bool applicationUserId_LockoutEnabled { get; set; }
        public int applicationUserId_AccessFailedCount { get; set; }
        public string departmentId_Name { get; set; }
        public bool? departmentId_IsActive { get; set; }
        public string departmentId_CreatedByUserId { get; set; }
        public object departmentId_UpdatedByUserId { get; set; }
        public object departmentId_DeletedByUserId { get; set; }
        public bool? departmentId_IsDeleted { get; set; }
        public string departmentId_Tags { get; set; }
        public string departmentId_Caption { get; set; }
        public string fullName { get; set; }
    }

}
