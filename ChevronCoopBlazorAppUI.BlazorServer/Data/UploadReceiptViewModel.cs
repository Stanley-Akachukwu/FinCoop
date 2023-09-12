namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class UploadReceiptViewModel
    {

        public byte[] Document { get; set; }

        public string MimeType { get; set; }

        public string FileName { get; set; }

        public double FileSize { get; set; }
    }
}
