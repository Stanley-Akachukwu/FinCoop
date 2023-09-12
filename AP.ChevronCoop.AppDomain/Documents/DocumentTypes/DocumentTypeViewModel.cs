using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Documents.DocumentTypes
{
    public partial class DocumentTypeViewModel : BaseViewModel
    {

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }


        [Required]
        public bool SystemFlag { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
