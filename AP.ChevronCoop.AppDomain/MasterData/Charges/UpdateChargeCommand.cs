using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.MasterData.Charges
{
    public partial class UpdateChargeCommand : UpdateCommand, IRequest<CommandResult<ChargeViewModel>>
    {

        [MaxLength(32)]
        [Required]
        public string Code { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        [MaxLength(32)]
        [Required]
        public string Method { get; set; }
        //public ChargeMethod Method { get; set; }

        [MaxLength(32)]
        [Required]
        public string Target { get; set; }
        //public ChargeTarget Target { get; set; }

        [MaxLength(32)]
        [Required]
        public string CalculationMethod { get; set; }
        //public ChargeCalculationMethod CalculationMethod { get; set; }

        [MaxLength(40)]
        [Required]
        public string CurrencyId { get; set; }

        //[Required]
        //public decimal FlatFee { get; set; }

        //[Required]
        //public decimal Percent { get; set; }


        [Required]
        public decimal ChargeValue { get; set; } = 0;
       
        public decimal? MaximumCharge { get; set; }
        public decimal? MinimimumCharge { get; set; }

    }







}
