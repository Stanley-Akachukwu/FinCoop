using AP.ChevronCoop.Entities.Loans.DepartmentLoanProductPublications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AP.ChevronCoop.Entities.Loans.LoanProductPublications.DepartmentLoanProductPublications;

public partial class DepartmentLoanProductPublicationConfiguration : BaseEntityConfiguration<DepartmentLoanProductPublication, string>
{
    public override void Configure(EntityTypeBuilder<DepartmentLoanProductPublication> entity)
    {
        base.Configure(entity);

        entity.ToTable(nameof(DepartmentLoanProductPublication), DbSchemaConstants.Loans);

        entity.Property(lpc => lpc.ProductId)
            .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lpc => lpc.Product)
          .WithMany()
          .HasForeignKey(lpc => lpc.ProductId)
          .IsRequired();

        entity.Property(lpc => lpc.DepartmentId)
          .HasColumnType("nvarchar")
          .HasMaxLength(40);

        entity.HasOne(lpc => lpc.Department)
          .WithMany()
          .HasForeignKey(lpc => lpc.DepartmentId)
          .IsRequired();

        entity.Property(e => e.PublicationType).HasMaxLength(100).HasConversion<string>();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<DepartmentLoanProductPublication> entity);
}