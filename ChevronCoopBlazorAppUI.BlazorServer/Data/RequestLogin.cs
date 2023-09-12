namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
	public class RequestLogin
	{
		public string Password { get; set; }
		public string Email { get; set; }
	}
	public class UpDateStatus
	{
		public string UserId { get; set; }
		public string Status { get; set; }
	}
}
