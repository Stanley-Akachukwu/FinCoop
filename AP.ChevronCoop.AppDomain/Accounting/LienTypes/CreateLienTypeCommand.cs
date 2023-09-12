using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.LienTypes
{
    public partial class CreateLienTypeCommand : CreateCommand, IRequest<CommandResult<LienTypeViewModel>>
    {

        [MaxLength(64)]
        [Required]
        public string Code { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

    }







}
