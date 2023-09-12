using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.PaymentModes
{
    public partial class CreatePaymentModeCommand : CreateCommand, IRequest<CommandResult<PaymentModeViewModel>>
    {

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        [MaxLength(64)]
        [Required]
        public string Channel { get; set; }
    }

}
