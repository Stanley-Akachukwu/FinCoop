using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text.Json;

namespace AP.ChevronCoop.Entities
{

    public abstract class BaseEntityConfiguration<TEntity, IdType> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<IdType>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> entity)
        {
            entity.UseTpcMappingStrategy();

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnType("nvarchar").HasMaxLength(40);

            entity.HasQueryFilter(t => t.IsDeleted == false);

            entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            // entity.Property(e => e.Tags)
            //     .HasConversion(
            //         v => JsonSerializer.Serialize(v, typeof(List<string>), JsonSerializerOptions.Default),
            //         v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default)
            //     );

        }


    }
}