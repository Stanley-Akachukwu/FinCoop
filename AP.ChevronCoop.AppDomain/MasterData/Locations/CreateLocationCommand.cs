using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.Locations
{
    public partial class CreateLocationCommand : CreateCommand, IRequest<CommandResult<LocationViewModel>>
    {

        [MaxLength(64)]
        [Required]
        public string LocationType { get; set; }

        [MaxLength(40)]

        public string ParentId { get; set; }

        [MaxLength(64)]
        [Required]
        public string Code { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool SystemFlag { get; set; }
 
    }







}
