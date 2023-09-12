using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.GlobalCodes
{
    public partial class CreateGlobalCodeCommand : CreateCommand, IRequest<CommandResult<GlobalCodeViewModel>>
    {

        [MaxLength(64)]
        [Required]
        public string CodeType { get; set; }

        [MaxLength(128)]
        [Required]
        public string Code { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool SystemFlag { get; set; }

        
    }







}
