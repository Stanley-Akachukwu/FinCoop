using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.PaymentModes
{
    public partial class PaymentModeViewModel : BaseViewModel
    {

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [MaxLength(64)]
        [Required]
        public string Channel { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
