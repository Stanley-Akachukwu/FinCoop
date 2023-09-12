using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalDocuments
{
    public partial class CreateApprovalDocumentCommand : CreateCommand, IRequest<CommandResult<ApprovalDocumentViewModel>>
    {

        [MaxLength(80)]
        [Required]
        public string ApprovalId { get; set; }

        [Required]
        public byte[] Evidence { get; set; }

        [MaxLength(128)]
        [Required]
        public string MimeType { get; set; }

        [MaxLength(512)]
        [Required]
        public string FileName { get; set; }

        [Required]
        public int FileSize { get; set; }

    }






}
