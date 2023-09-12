using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.MasterData.Banks
{
    public partial class BankViewModel : BaseViewModel
    {

        [MaxLength(32)]
        [Required]
        public string Code { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Address { get; set; }
        [MaxLength(256)]
        public string ContactName { get; set; }
        [MaxLength(256)]
        public string ContactDetails { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
