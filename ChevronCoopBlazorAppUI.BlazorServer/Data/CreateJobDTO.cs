namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class CreateJobDTO
    {
        public string CronJobType { get; set; }

        public string JobName { get; set; }

        public DateTime ProcessingDate { get; set; }
        public DateTime ProcessingTime { get; set; }
    }
}
