using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain
{
    public abstract class DeleteCommand
    {
        [Required]
        public virtual string Id { get; set; }

        //public string? DeletedBy { get; set; }
        [MaxLength(128)]
        public string DeletedByUserId { get; set; }
        //public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DateDeleted { get; set; }

        //[ConcurrencyCheck]
        public Guid RowVersion { get; set; }
    }
}
