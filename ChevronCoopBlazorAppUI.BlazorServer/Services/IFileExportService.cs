using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using ChevronCoop.Web.AppUI.BlazorServer.Data;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
    public interface IFileExportService
    {
        Task ExportDataMigrationToCSV(List<MemberDataUpload> model, bool isValid);
        Task DownloadTemplateDataMigrationToCSV(List<MemberUploadTemplateModelDTO> model);
    }
}
