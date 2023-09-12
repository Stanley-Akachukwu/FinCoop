using AP.ChevronCoop.Entities.Security.AuditTrails;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserTokens;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Microsoft.EntityFrameworkCore;

namespace AP.ChevronCoop.Entities;

public partial class ChevronCoopDbContext //: DbContext
{
  public DbSet<MemberProfile> MemberProfiles { get; set; }
  public DbSet<MemberProfileMasterView> MemberProfileMasterView { get; set; }
  public DbSet<MemberBankAccount> MemberBankAccounts { get; set; }
  public DbSet<MemberBankAccountMasterView> MemberBankAccountMasterView { get; set; }
  public DbSet<MemberNextOfKin> MemberNextOfKins { get; set; }
  public DbSet<MemberNextOfKinMasterView> MemberNextOfKinMasterView { get; set; }
  public DbSet<MemberBeneficiary> MemberBeneficiaries { get; set; }
  public DbSet<MemberBeneficiaryMasterView> MemberBeneficiaryMasterView { get; set; }
  public DbSet<EnrollmentPaymentInfo> EnrollmentPaymentInfos { get; set; }
  public DbSet<EnrollmentPaymentInfoMasterView> EnrollmentPaymentInfoMasterView { get; set; }
  public DbSet<ApplicationRole> ApplicationRoles { get; set; }
  public DbSet<ApplicationRoleMasterView> ApplicationRoleMasterView { get; set; }
  public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }
  public DbSet<ApplicationRoleClaimMasterView> ApplicationRoleClaimMasterView { get; set; }
  public DbSet<ApplicationUser> ApplicationUsers { get; set; }
  public DbSet<ApplicationUserMasterView> ApplicationUserMasterView { get; set; }
  public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
  public DbSet<ApplicationUserClaimMasterView> ApplicationUserClaimMasterView { get; set; }
  public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
  public DbSet<ApplicationUserLoginMasterView> ApplicationUserLoginMasterView { get; set; }
  public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
  public DbSet<ApplicationUserRoleMasterView> ApplicationUserRoleMasterView { get; set; }
  public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }
  public DbSet<ApplicationUserTokenMasterView> ApplicationUserTokenMasterView { get; set; }
  public DbSet<Permission> Permissions { get; set; }
  public DbSet<PermissionMasterView> PermissionMasterView { get; set; }

  // public DbSet<RetireeSwitch> RetireeSwitches { get; set; }
  // public DbSet<RetireeSwitchMasterView> RetireeSwitchMasterView { get; set; }
  public DbSet<AuditTrail> AuditTrails { get; set; }
  public DbSet<AuditTrailMasterView> AuditTrailMasterView { get; set; }
  public DbSet<MemberBulkUploadTemp> MemberBulkUploadTemp { get; set; }
  public DbSet<MemberBulkUploadSessionMasterView> MemberBulkUploadSessionMasterView { get; set; }
  public DbSet<MemberBulkUploadSession> MemberBulkUploadSessions { get; set; }
  public DbSet<MemberProfileViaUploadMasterView> MemberProfileViaUploadMasterView { get; set; }


  public void OnModelCreating_Security(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new MemberProfileConfiguration());
    modelBuilder.ApplyConfiguration(new MemberBeneficiaryConfiguration());
    modelBuilder.ApplyConfiguration(new MemberNextOfKinConfiguration());
    // modelBuilder.ApplyConfiguration(new RetireeSwitchConfiguration());
    modelBuilder.ApplyConfiguration(new AuditTrailConfiguration());
    modelBuilder.ApplyConfiguration(new MemberBulkUploadTempConfiguration());
    modelBuilder.ApplyConfiguration(new MemberBulkUploadSessionConfiguration());
    modelBuilder.ApplyConfiguration(new MemberBankAccountConfiguration());


    modelBuilder.Entity<ApplicationUser>(b =>
    {
      b.ToTable(nameof(ApplicationUser), DbSchemaConstants.Security);
      // Each User can have many UserClaims
      b.HasMany(e => e.Claims)
        .WithOne(e => e.User)
        .HasForeignKey(uc => uc.UserId)
        .IsRequired();

      // Each User can have many UserLogins
      b.HasMany(e => e.Logins)
        .WithOne(e => e.User)
        .HasForeignKey(ul => ul.UserId)
        .IsRequired();

      // Each User can have many UserTokens
      b.HasMany(e => e.Tokens)
        .WithOne(e => e.User)
        .HasForeignKey(ut => ut.UserId)
        .IsRequired();

      // Each User can have many entries in the UserRole join table
      b.HasMany(e => e.UserRoles)
        .WithOne(e => e.User)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();


      b.HasOne(e => e.MemberProfile)
        .WithOne(e => e.ApplicationUser)
        .HasForeignKey<MemberProfile>(p => p.ApplicationUserId)
        .IsRequired();
    });

    modelBuilder.Entity<ApplicationUserMasterView>(b =>
    {
      b.ToTable(nameof(ApplicationUserMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<ApplicationUserClaim>(b =>
    {
      b.ToTable(nameof(ApplicationUserClaim), DbSchemaConstants.Security);
    });

    modelBuilder.Entity<ApplicationUserClaimMasterView>(b =>
    {
      b.ToTable(nameof(ApplicationUserClaimMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });


    modelBuilder.Entity<ApplicationUserLogin>(b =>
    {
      b.ToTable(nameof(ApplicationUserLogin), DbSchemaConstants.Security);
    });

    modelBuilder.Entity<ApplicationUserLoginMasterView>(b =>
    {
      b.ToTable(nameof(ApplicationUserLoginMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.UserId);
    });

    modelBuilder.Entity<ApplicationUserToken>(b =>
    {
      b.ToTable(nameof(ApplicationUserToken), DbSchemaConstants.Security);
    });

    modelBuilder.Entity<ApplicationUserTokenMasterView>(b =>
    {
      b.ToTable(nameof(ApplicationUserTokenMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.UserId);
    });

    modelBuilder.Entity<MemberBeneficiaryMasterView>(b =>
    {
      b.ToTable(nameof(MemberBeneficiaryMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<MemberNextOfKinMasterView>(b =>
    {
      b.ToTable(nameof(MemberNextOfKinMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<ApplicationRole>(b =>
    {
      b.ToTable(nameof(ApplicationRole), DbSchemaConstants.Security);

      // Each Role can have many entries in the UserRole join table
      b.HasMany(e => e.UserRoles)
        .WithOne(e => e.Role)
        .HasForeignKey(ur => ur.RoleId)
        .IsRequired();

      // Each Role can have many associated RoleClaims
      b.HasMany(e => e.RoleClaims)
        .WithOne(e => e.Role)
        .HasForeignKey(rc => rc.RoleId)
        .IsRequired();
    });

    modelBuilder.Entity<ApplicationRoleMasterView>(b =>
    {
      b.ToTable(nameof(ApplicationRoleMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<AuditTrail>(b => { b.ToTable(nameof(AuditTrail), DbSchemaConstants.Security); });
    modelBuilder.Entity<MemberBulkUploadTemp>(b =>
    {
      b.ToTable(nameof(MemberBulkUploadTemp), DbSchemaConstants.Security);
    });

    modelBuilder.Entity<AuditTrailMasterView>(b =>
    {
      b.ToTable(nameof(AuditTrailMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });
    modelBuilder.Entity<MemberBulkUploadSessionMasterView>(b =>
    {
      b.ToTable(nameof(MemberBulkUploadSessionMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.MemberBulkUploadSessionId);
    });
    modelBuilder.Entity<ApplicationUserRole>(b =>
    {
      b.ToTable(nameof(ApplicationUserRole), DbSchemaConstants.Security);
    });

    modelBuilder.Entity<ApplicationUserRoleMasterView>(b =>
    {
      b.ToTable(nameof(ApplicationUserRoleMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => new { e.RoleId, e.UserId });
    });

    modelBuilder.Entity<ApplicationRoleClaim>(b =>
    {
      b.ToTable(nameof(ApplicationRoleClaim), DbSchemaConstants.Security);
    });

    modelBuilder.Entity<ApplicationRoleClaimMasterView>(b =>
    {
      b.ToTable(nameof(ApplicationRoleClaimMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });


    modelBuilder.Entity<EnrollmentPaymentInfo>(b =>
    {
      b.ToTable(nameof(EnrollmentPaymentInfo), DbSchemaConstants.Security);

      b.Property(t => t.MimeType).HasColumnType("nvarchar").HasMaxLength(64).IsRequired();
      b.Property(t => t.FileName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
      b.Property(t => t.Evidence).IsRequired();
    });

    modelBuilder.Entity<EnrollmentPaymentInfoMasterView>(b =>
    {
      b.ToTable(nameof(EnrollmentPaymentInfoMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<MemberProfileMasterView>(b =>
    {
      b.ToTable(nameof(MemberProfileMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });

    modelBuilder.Entity<Permission>(entity =>
    {
      entity.ToTable(nameof(Permission), DbSchemaConstants.Security);

      entity.Property(e => e.Id).HasColumnType("nvarchar").HasMaxLength(40);

      entity.HasIndex(x => new { x.Name, x.Code }).IsUnique();
      entity.HasIndex(x => new { x.Name, x.Code, x.Module }).IsUnique();
      entity.HasIndex(x => new { x.Name, x.Code, x.Category }).IsUnique();
      entity.HasIndex(x => new { x.Name, x.Code, x.Group }).IsUnique();

      entity.Property(t => t.Code).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
      entity.Property(t => t.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();

      entity.Property(t => t.Category).HasColumnType("nvarchar").HasMaxLength(256);
      entity.Property(t => t.Group).HasColumnType("nvarchar").HasMaxLength(256);
      entity.Property(t => t.Module).HasColumnType("nvarchar").HasMaxLength(256);
      entity.Property(t => t.Owner).HasColumnType("nvarchar").HasMaxLength(256);
    });

    modelBuilder.Entity<PermissionMasterView>(b =>
    {
      b.ToTable(nameof(PermissionMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });
    
    modelBuilder.Entity<MemberProfileViaUploadMasterView>(b =>
    {
      b.ToTable(nameof(MemberProfileViaUploadMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });
    
    modelBuilder.Entity<MemberBankAccountMasterView>(b =>
    {
      b.ToTable(nameof(MemberBankAccountMasterView), DbSchemaConstants.Security, t => t.ExcludeFromMigrations());
      b.HasKey(e => e.Id);
    });
  }
}