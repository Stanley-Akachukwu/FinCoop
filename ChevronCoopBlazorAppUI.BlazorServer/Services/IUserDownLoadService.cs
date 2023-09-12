namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
	public interface IUserDownLoadService
	{
		Task ExportUsersToCSV();
        Task ExportUsersWhoHaveNotCompletedKYCToCSV();
        Task ExportUsersWithStatusToCSV(string status);

    }
}