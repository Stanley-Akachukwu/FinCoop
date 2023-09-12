using AP.ChevronCoop.Commons;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Audit.EntityFramework;

namespace AP.ChevronCoop.Entities
{


    //public abstract class BaseEntity
    //{

    //}

    [AuditInclude]
    public abstract class BaseEntity<T> //: BaseEntity
    {

        public BaseEntity()
        {
            //if (Id is string)
            //    Id = NUlid.Ulid.NewUlid().ToString();
            //else if (Id is Guid)
            //    Id = NUlid.Ulid.NewUlid().ToGuid();

            //Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString();
            RowVersion = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid);

        }


        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public T Id { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        //public string? CreatedBy { get; set; }
        [MaxLength(128)]
        public string CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; } = DateTime.Now;



        //public string? UpdatedBy { get; set; }
        [MaxLength(128)]
        public string UpdatedByUserId { get; set; }
        public DateTimeOffset? DateUpdated { get; set; } = DateTime.Now;



        //public string? DeletedBy { get; set; }
        [MaxLength(128)]
        public string DeletedByUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DateDeleted { get; set; }

        [ConcurrencyCheck]
        public Guid RowVersion { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        [MaxLength(512)]
        public string FullText { get; set; }

        //[MaxLength(512)]
        // public List<string> Tags { get; set; } = new();
        public string Tags { get; set; }

        [MaxLength(256)]
        public string? Caption { get; set; } //computed column



        [NotMapped]
        public abstract string DisplayCaption
        {
            get;
        }


        [NotMapped]
        public abstract string DropdownCaption
        {
            get;
        }

        [NotMapped]
        public abstract string ShortCaption
        {
            get;
        }

    }
}