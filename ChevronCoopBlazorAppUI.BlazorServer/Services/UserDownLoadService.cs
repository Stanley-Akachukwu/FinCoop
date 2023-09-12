using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
	public class UserDownLoadService : IUserDownLoadService
	{
		private readonly NavigationManager navigationManager;
		 
		public UserDownLoadService(NavigationManager navigationManager)
		{
			this.navigationManager = navigationManager;
			
		}

        public async Task ExportUsersWithStatusToCSV(string status)
        {
			//var redirectURL = "/export/userswithstatus/csv?status="+status;
			var redirectURL = $"/export/users/csv(status='{status}')";
            navigationManager.NavigateTo(redirectURL, true);
        }

        public async Task ExportUsersToCSV()
		{
			var redirectURL = "/export/users/csv";
			navigationManager.NavigateTo(redirectURL, true);
		}

       public async Task ExportUsersWhoHaveNotCompletedKYCToCSV()
		{

			var redirectURL = "/export/users/notcompletedkyc/csv";
            navigationManager.NavigateTo(redirectURL, true);
        }
    }
}
