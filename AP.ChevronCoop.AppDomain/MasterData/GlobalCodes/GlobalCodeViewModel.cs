using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.GlobalCodes
{
    public partial class GlobalCodeViewModel : BaseViewModel
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
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
