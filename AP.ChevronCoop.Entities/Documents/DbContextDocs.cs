using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.Documents.DocumentTypes;
using AP.ChevronCoop.Entities.Documents.OfficeDocuments;
using AP.ChevronCoop.Entities.Documents.OfficePhotos;
using AP.ChevronCoop.Entities.Documents.OfficeSheets;
using AP.ChevronCoop.Entities.Documents.PayrollDeductionDocuments;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities
{
    public partial class ChevronCoopDbContext //: DbContext
    {
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<OfficeDocument> OfficeDocuments { get; set; }
        public DbSet<OfficeSheet> OfficeSheets { get; set; }
        public DbSet<OfficePhoto> OfficePhotos { get; set; }
        public DbSet<CustomerPaymentDocument> CustomerPaymentDocuments { get; set; }
        public DbSet<CustomerPaymentDocumentMasterView> CustomerPaymentDocumentMasterView { get; set; }

        public DbSet<OfficeDocumentMasterView> OfficeDocumentMasterView { get; set; }
        public DbSet<OfficePhotoMasterView> OfficePhotoMasterView { get; set; }
        public DbSet<OfficeSheetMasterView> OfficeSheetMasterView { get; set; }
        public DbSet<DocumentTypeMasterView> DocumentTypeMasterView { get; set; }
        public DbSet<PayrollDeductionDocumentMasterView> PayrollDeductionDocumentMasterView { get; set; }

        public DbSet<PayrollDeductionDocument> PayrollDeductionDocuments { get; set; }

        public void OnModelCreating_Documents(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeSheetConfiguration());
            modelBuilder.ApplyConfiguration(new OfficePhotoConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new PayrollDeductionDocumentConfiguration());


            modelBuilder.Entity<OfficeDocumentMasterView>(b =>
            {
                b.ToTable(nameof(OfficeDocumentMasterView), DbSchemaConstants.Documents, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<DocumentTypeMasterView>(b =>
            {
                b.ToTable(nameof(DocumentTypeMasterView), DbSchemaConstants.Documents, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<OfficePhotoMasterView>(b =>
            {
                b.ToTable(nameof(OfficePhotoMasterView), DbSchemaConstants.Documents, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<OfficeSheetMasterView>(b =>
            {
                b.ToTable(nameof(OfficeSheetMasterView), DbSchemaConstants.Documents, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });


            modelBuilder.Entity<CustomerPaymentDocumentMasterView>(b =>
            {
                b.ToTable(nameof(CustomerPaymentDocumentMasterView), DbSchemaConstants.Documents, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<PayrollDeductionDocumentMasterView>(b =>
            {
                b.ToTable(nameof(PayrollDeductionDocumentMasterView), DbSchemaConstants.Documents, buildAction: t => t.ExcludeFromMigrations());
                b.HasKey(e => e.Id);
            });
        }
    }
}