using AP.ChevronCoop.Entities.Security.Alerts.EmailAlerts;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalDocuments;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupMembers;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroupWorkflows;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalLogs;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities;

public partial class ChevronCoopDbContext //: DbContext
{
    public DbSet<ApprovalLog> ApprovalLogs { get; set; }
    public DbSet<ApprovalLogMasterView> ApprovalLogMasterView { get; set; }
    public DbSet<Approval> Approvals { get; set; }
    public DbSet<ApprovalMasterView> ApprovalMasterView { get; set; }
    public DbSet<ApprovalView> ApprovalView { get; set; }
    public DbSet<ApprovalStatsMasterView> ApprovalStatsMasterView { get; set; }
    public DbSet<ApprovalRole> ApprovalRoles { get; set; }
    public DbSet<ApprovalRoleMasterView> ApprovalRoleMasterView { get; set; }
    public DbSet<ApprovalDocument> ApprovalDocuments { get; set; }
    public DbSet<ApprovalDocumentMasterView> ApprovalDocumentMasterView { get; set; }
    public DbSet<ApprovalWorkflowMasterView> ApprovalWorkflowMasterView { get; set; }
    public DbSet<ApprovalEmailAlert> ApprovalEmailAlerts { get; set; }
    public DbSet<ApprovalGroup> ApprovalGroups { get; set; }
    public DbSet<ApprovalWorkflow> ApprovalWorkflows { get; set; }

    public DbSet<ApprovalGroupMember> ApprovalGroupMembers { get; set; }
    public DbSet<ApprovalGroupMemberMasterView> ApprovalGroupMemberMasterView { get; set; }
    public DbSet<ApprovalGroupWorkflow> ApprovalGroupWorkflows { get; set; }
    // public DbSet<MemberApprovalAction> MemberApprovalActions { get; set; }

    public DbSet<ApprovalGroupMasterView> ApprovalGroupMasterView { get; set; }
    public DbSet<ApprovalNotification> ApprovalNotifications { get; set; }

    public void OnModelCreating_Security_Approvals(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApprovalConfiguration());
        modelBuilder.ApplyConfiguration(new ApprovalWorkflowConfiguration());
        modelBuilder.ApplyConfiguration(new ApprovalEmailAlertConfiguration());
        modelBuilder.ApplyConfiguration(new ApprovalGroupConfiguration());
        modelBuilder.ApplyConfiguration(new ApprovalGroupMemberConfiguration());
        modelBuilder.ApplyConfiguration(new ApprovalLogConfiguration());
        modelBuilder.ApplyConfiguration(new ApprovalGroupWorkflowConfiguration());
        modelBuilder.ApplyConfiguration(new ApprovalNotificationConfiguration());
        // modelBuilder.ApplyConfiguration(new MemberApprovalActionConfiguration());

        //modelBuilder.Entity<ApprovalNotification>()
        //      .OwnsOne(approvalNotification => approvalNotification.Escalation, builder => { builder.ToJson(); })
        //     .OwnsOne(approvalNotification => approvalNotification.Reminder, builder => { builder.ToJson(); });



        modelBuilder.Entity<ApprovalMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        
        modelBuilder.Entity<ApprovalView>(b =>
        {
            b.ToTable(nameof(ApprovalView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<ApprovalStatsMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalStatsMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.RowNumber);
        });

        modelBuilder.Entity<ApprovalRole>(entity =>
        {
            entity.ToTable(nameof(ApprovalRole), DbSchemaConstants.Security);

            entity.Property(e => e.Id).HasColumnType("nvarchar").HasMaxLength(40);

            entity.HasIndex(x => new { x.EventGlobalCodeId, x.RoleId }).IsUnique();
            entity.HasIndex(x => new { x.EventGlobalCodeId, x.RoleId, x.Order }).IsUnique();

            entity.Property(t => t.Order).IsRequired();
        });

        modelBuilder.Entity<ApprovalRoleMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalRoleMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<ApprovalDocument>(entity =>
        {
            entity.ToTable(nameof(ApprovalDocument), DbSchemaConstants.Security);
            entity.Property(e => e.Id).HasColumnType("nvarchar").HasMaxLength(40);
            entity.Property(t => t.MimeType).HasColumnType("nvarchar").HasMaxLength(64).IsRequired();
            entity.Property(t => t.FileName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            entity.Property(t => t.Document).IsRequired();
        });

        modelBuilder.Entity<ApprovalDocumentMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalDocumentMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<ApprovalWorkflowMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalWorkflowMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<ApprovalGroupMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalGroupMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });


        modelBuilder.Entity<ApprovalGroupMemberMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalGroupMemberMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<ApprovalLogMasterView>(b =>
        {
            b.ToTable(nameof(ApprovalLogMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
            b.HasKey(e => e.Id);
        });
    }
}