using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.Charges
{
    public partial class ChargeViewModel : BaseViewModel
    {

        [MaxLength(64)]
        [Required]
        public string Code { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [MaxLength(32)]
        [Required]
        public string Method { get; set; }

        [MaxLength(32)]
        [Required]
        public string Target { get; set; }

        [MaxLength(32)]
        [Required]
        public string CalculationMethod { get; set; }

        [MaxLength(80)]
        [Required]
        public string CurrencyId { get; set; }


        [Required]
        public decimal ChargeValue { get; set; } = 0;

        public decimal? MaximumCharge { get; set; }
        public decimal? MinimimumCharge { get; set; }


        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
