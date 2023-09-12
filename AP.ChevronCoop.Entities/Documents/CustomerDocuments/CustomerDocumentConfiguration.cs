using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Documents.CustomerDocuments;

public partial class CustomerDocumentConfiguration : BaseEntityConfiguration<CustomerPaymentDocument, string>
{
    public override void Configure(EntityTypeBuilder<CustomerPaymentDocument> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(CustomerPaymentDocument), DbSchemaConstants.Documents);

        entity.Property(e => e.FileName)
          .HasColumnType("nvarchar(max)");
        
        entity.Property(d => d.Document)
          .HasColumnType("nvarchar(max)")
          .HasColumnName("DocumentData");

        entity.HasOne(e => e.Customer)
          .WithMany()
          .HasForeignKey(e => e.CustomerId);
       
        entity.Property(e => e.DocumentType).HasMaxLength(32).HasConversion<string>();


        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<CustomerPaymentDocument> entity);
}