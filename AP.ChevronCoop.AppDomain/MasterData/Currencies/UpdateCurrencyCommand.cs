using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.Currencies
{
    public partial class UpdateCurrencyCommand : UpdateCommand, IRequest<CommandResult<CurrencyViewModel>>
    {

        [MaxLength(16)]
        [Required]
        public string Code { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        [MaxLength(16)]
        [Required]
        public string Symbol { get; set; }

        [MaxLength(20)]

        public string IsoSymbol { get; set; }

        [Required]
        public int DecimalPlaces { get; set; }

        [MaxLength(32)]

        public string Format { get; set; }

        
    }







}
