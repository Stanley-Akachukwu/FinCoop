using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.Locations
{
    public partial class LocationViewModel : BaseViewModel
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
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
