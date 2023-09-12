namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class BankViewResult
    {
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string contactName { get; set; }
        public string contactDetails { get; set; }
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
        public object tags { get; set; }
        public object caption { get; set; }
    }
}
