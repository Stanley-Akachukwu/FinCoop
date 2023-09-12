using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AP.ChevronCoop.AppDomain
{
    public abstract class CreateCommand
    {

        [MaxLength(512)]
        public string? Description { get; set; }

        [MaxLength(512)]
        public string? Comments { get; set; }

        public bool IsActive { get; set; } = true;

      

        [MaxLength(512)]
        [Browsable(false)]
        public string? FullText { get; set; }

        [MaxLength(256)]
        //public List<string> Tags { get; set; }
        public string? Tags { get; set; }

        [MaxLength(256)]
        public string? Caption { get; set; } //computed column

        [MaxLength(128)]
        public string CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; } = DateTime.Now;



    }
}
