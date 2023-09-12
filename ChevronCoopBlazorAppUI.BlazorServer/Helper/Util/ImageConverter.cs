using Syncfusion.Blazor.Inputs;

namespace ChevronCoop.Web.AppUI.BlazorServer.Helper.Util
{
    public static class UploadUtility
    {
        private static string ConvertFileToBase64(UploadFiles file)
        {
            byte[] bytes = file.Stream.ToArray();

            string base64 = Convert.ToBase64String(bytes);

            return @"data:image/" + file.FileInfo.Type + ";base64," + base64;
        }
        private static string ConvertFileToBase64Sbyte(UploadFiles file)
        {
            byte[] bytes = file.Stream.ToArray();

            string base64 = Convert.ToBase64String(bytes);

            return @"data:image/" + file.FileInfo.Type + ";base64," + base64;
        }
        public static string ValidateFile(UploadFiles file)
        {
            List<string> allowedFileTypes = new List<string> { "jpg", "jpeg", "png", "pdf" };
            string fileType = file.FileInfo.Type.ToLower();
            long maxSizeInBytes = 10 * 1024 * 1024;

            if (!allowedFileTypes.Contains(fileType))
            {
                return $"This file type '{fileType}' is not allowed";
            }

            if (file.FileInfo.Size > maxSizeInBytes)
            {
                return $"This file size '{file.FileInfo.Size}' is not allowed";
            }

            return "";
        }
        public static string DocumentViewer(byte[] document, string type)
        {
            string base64 = Convert.ToBase64String(document);
            string base64PDF = "";
            if (type.Equals("pdf"))
            {
               return base64PDF = @"data:application/pdf;base64," + base64;
            }
            return @"data:image/" + type + ";base64," + base64;
        }

    }
}
