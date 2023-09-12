using Syncfusion.Blazor.Inputs;

namespace ChevronCoop.Web.AppUI.BlazorServer.Helper
{
    public static class ImageConverterHelper
    {
        public static string ConvertFileToBase64(UploadFiles file)
        {
            byte[] bytes = file.Stream.ToArray();

            string base64 = Convert.ToBase64String(bytes);

            return @"data:image/" + file.FileInfo.Type + ";base64," + base64;
        }

        public static string ValidateDocuments(UploadFiles file)
        {
            List<string> allowedFileType = new List<string>();
            allowedFileType.Add("jpg");
            allowedFileType.Add("jpeg");
            allowedFileType.Add("png");
            allowedFileType.Add("pdf");
            if (!allowedFileType.Contains(file.FileInfo.Type.ToLower()))
                return $"This file type {file.FileInfo.Type} is not allowed";
            if (file.FileInfo.Size > (10 * 1024 * 1024))
                return $"This file size {file.FileInfo.Size * (1024 * 1024)} is not allowed";
            return "";

        }

    }
}
