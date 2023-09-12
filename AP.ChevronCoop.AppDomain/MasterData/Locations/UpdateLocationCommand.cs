using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.Locations
{
    public partial class UpdateLocationCommand : UpdateCommand, IRequest<CommandResult<LocationViewModel>>
    {

        [MaxLength(64)]
        [Required]
        public string LocationType { get; set; }

        [MaxLength(900)]

        public string ParentId { get; set; }

        [MaxLength(128)]
        [Required]
        public string Code { get; set; }

        [MaxLength(512)]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool SystemFlag { get; set; }

        
    }







}
