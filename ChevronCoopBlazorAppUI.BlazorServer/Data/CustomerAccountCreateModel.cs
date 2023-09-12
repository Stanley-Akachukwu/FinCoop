namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class CustomerAccountCreateModel
    {
        public string description { get; set; }
        public string comments { get; set; }
        public bool isActive { get; set; }
        public string fullText { get; set; }
        public string tags { get; set; }
        public string caption { get; set; }
        public string createdByUserId { get; set; }
        public DateTime dateCreated { get; set; }
        public string profileId { get; set; }
        public string bankId { get; set; }
        public string bvn { get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public string branch { get; set; }
    }
}
