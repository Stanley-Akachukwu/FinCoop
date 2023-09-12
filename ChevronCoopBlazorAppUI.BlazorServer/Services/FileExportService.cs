using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Web;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
    public class FileExportService : IFileExportService
    {
        private readonly NavigationManager navigationManager;

        public FileExportService(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
        public async Task ExportDataMigrationToCSV(List<MemberDataUpload> model, bool isValid)
        {
            if (isValid)
            {
                var serializedObjectList = JsonSerializer.Serialize(model);
                string modelList = HttpUtility.UrlEncode(serializedObjectList);
                var redirectURL = $"/export/DataMigration/csv(modelList='{modelList}')";
                navigationManager.NavigateTo(redirectURL, true);
            }
            else
            {
                var serializedObjectList = JsonSerializer.Serialize(model);
                string modelList = HttpUtility.UrlEncode(serializedObjectList);
                var redirectURL = $"/export/InvalidDataMigration/csv(modelList='{modelList}')";
                navigationManager.NavigateTo(redirectURL, true);
            }

        }
        public async Task DownloadTemplateDataMigrationToCSV(List<MemberUploadTemplateModelDTO> model)
        {

            var serializedObjectList = JsonSerializer.Serialize(model);
            string modelList = HttpUtility.UrlEncode(serializedObjectList);
            var redirectURL = $"/export/Template/csv(modelList='{modelList}')";
            navigationManager.NavigateTo(redirectURL, true);


        }
    }
}
