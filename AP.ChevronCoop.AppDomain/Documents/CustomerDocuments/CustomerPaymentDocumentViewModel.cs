namespace AP.ChevronCoop.AppDomain.Documents.CustomerDocuments
{
    public class CustomerPaymentDocumentViewModel: BaseViewModel
    {

        public string CustomerId { get; set; }
        public string Document { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
    }
}
