using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Documents.OfficePhotos
{
    public partial class OfficePhotoViewModel : BaseViewModel
    {

        [MaxLength(64)]
        [Required]
        public string DocumentNo { get; set; }

        [MaxLength(80)]
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
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
