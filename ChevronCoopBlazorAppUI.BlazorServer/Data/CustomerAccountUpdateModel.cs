namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class CustomerAccountUpdateModel
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
        public DateTime dateDeleted { get; set; }
        public string rowVersion { get; set; }
        public string fullText { get; set; }
        public string tags { get; set; }
        public string caption { get; set; }
        public string profileId { get; set; }
        public string bankId { get; set; }
        public string bvn { get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public string branch { get; set; }
        public string sortCode { get; set; }
    }
}
