using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Documents.OfficeDocuments
{
    public partial class CreateOfficeDocumentCommand : CreateCommand, IRequest<CommandResult<OfficeDocumentViewModel>>
    {

        [MaxLength(64)]
        [Required]
        public string DocumentNo { get; set; }

        [MaxLength(40)]
        [Required]
        public string DocumentTypeId { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }


        public byte[] Document { get; set; }

        [MaxLength(64)]

        public string MimeType { get; set; }

        [MaxLength(400)]

        public string FilePath { get; set; }

         
    }







}
