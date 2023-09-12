using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Documents.OfficePhotos
{
    public partial class CreateOfficePhotoCommand : CreateCommand, IRequest<CommandResult<OfficePhotoViewModel>>
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
