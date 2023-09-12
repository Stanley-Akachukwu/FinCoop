namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class BeneficiaryDeleteModel
    {
        public string id { get; set; }
        public string deletedByUserId { get; set; }
        public DateTime dateDeleted { get; set; }
        public string rowVersion
        {
            get; set;
        }
    }
}
