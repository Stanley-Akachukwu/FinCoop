using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.LienTypes
{
    public partial class LienTypeViewModel : BaseViewModel
    {

        [MaxLength(100)]
        [Required]
        public string Code { get; set; }

        [MaxLength(500)]
        [Required]
        public string Name { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
