using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment
{
    public partial class EnrolmentGrid
    {
        [Inject]
        public IUserDownLoadService UserDownLoadService { get; set; }

        private async Task DownloadList()
        {
            var status = EnrolmentFIlter;
            await UserDownLoadService.ExportUsersWithStatusToCSV(status);
        }
    }
}