using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;

namespace ChevronCoop.Web.AppUI.BlazorServer.Controllers
{
    public class FileExportController : ExportController
    {
        [HttpGet("/export/DataMigration/csv(modelList='{modelListString}')")]
        public async Task<FileStreamResult> ExportDataMigrationToCSV(string modelListString)
        {
            var decodedValue = HttpUtility.UrlDecode(modelListString);
            var decodedValueValid = JsonSerializer.Deserialize<List<MemberDataUpload>>(decodedValue);
            var fileName = "Valid_Entries";

            if (decodedValueValid.Any())
            {
                var querableList = decodedValueValid.AsQueryable();
                return ToCSV(ApplyQuery(querableList, Request.Query), fileName);
            }
            else
            {
                var newList = new List<MemberDataUpload>();
                var querableList = newList.AsQueryable();
                return ToCSV(ApplyQuery(querableList, Request.Query), fileName);
            }
        }
        [HttpGet("/export/InvalidDataMigration/csv(modelList='{modelListString}')")]
        public async Task<FileStreamResult> ExportInvalidDataMigrationToCSV(string modelListString)
        {
            var decodedValue = HttpUtility.UrlDecode(modelListString);
            var decodedValueValid = JsonSerializer.Deserialize<List<MemberDataUpload>>(decodedValue);
            var fileName = "Invalid_Entries";

            if (decodedValueValid.Any())
            {
                var querableList = decodedValueValid.AsQueryable();
                return ToCSV(ApplyQuery(querableList, Request.Query), fileName);
            }
            else
            {
                var newList = new List<MemberDataUpload>();
                var querableList = newList.AsQueryable();
                return ToCSV(ApplyQuery(querableList, Request.Query), fileName);
            }
        }
        [HttpGet("/export/Template/csv(modelList='{modelListString}')")]
        public async Task<FileStreamResult> ExportTemplateToCSV(string modelListString)
        {
            var decodedValue = HttpUtility.UrlDecode(modelListString);
            var decodedValueValid = JsonSerializer.Deserialize<List<MemberUploadTemplateModelDTO>>(decodedValue);
            var fileName = "Bulk_Upload_Template";

            if (decodedValueValid.Any())
            {
                var querableList = decodedValueValid.AsQueryable();
                return ToCSV(ApplyQuery(querableList, Request.Query), fileName);
            }
            else
            {
                var newList = new List<MemberUploadTemplateModelDTO>();
                var querableList = newList.AsQueryable();
                return ToCSV(ApplyQuery(querableList, Request.Query), fileName);
            }
        }
    }
}
