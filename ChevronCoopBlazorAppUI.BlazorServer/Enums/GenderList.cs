namespace ChevronCoop.Web.AppUI.BlazorServer.Enum
{
	public class GenderList
	{
		public string ID { get; set; }
		public string Text { get; set; }
	}
	public class Status
	{
		public string ID { get; set; }
		public string Text { get; set; }
	}
	public enum ApprovalStatusDTO
	{
		APPROVED = 4,
		REJECTED = 5
	}
}
