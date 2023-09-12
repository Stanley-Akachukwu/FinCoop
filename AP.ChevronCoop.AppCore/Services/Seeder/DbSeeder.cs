using System.Reflection;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace AP.ChevronCoop.AppCore.Services.Seeder;

public class DbSeeder : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DbSeeder> logger;
    public DbSeeder(IServiceScopeFactory scopeFactory, ILogger<DbSeeder> _Logger)
    {
        _scopeFactory = scopeFactory;
        logger = _Logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {

        logger.LogInformation("starting DbSeeder");

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<ChevronCoopDbContext>();
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var appsettings = scope.ServiceProvider.GetRequiredService<IOptions<CoreAppSettings>>();
            var _appsettings = appsettings.Value;

            // Setup Permissions
            var existingPermissionsCode = await _dbContext.Permissions.Select(x => x.Code).ToListAsync(cancellationToken: cancellationToken);
            var permissions = SetupPermissions(existingPermissionsCode);
            await _dbContext.Permissions.AddRangeAsync(permissions, cancellationToken);

            // Setup system roles and role permissions
            // var existingRolesCode = await _dbContext.ApplicationRoles.Select(x => x.Code).ToListAsync(cancellationToken: cancellationToken);
            // var roles = SetupRoles(existingRolesCode);
            // await _dbContext.ApplicationRoles.AddRangeAsync(roles, cancellationToken);



            var roleClaims = await SetupSuperAdminRoleClaims(_dbContext);
            await _dbContext.ApplicationRoleClaims.AddRangeAsync(roleClaims, cancellationToken);

            // await SetupSuperAdmin(_dbContext, _userManager, _appsettings);
            SeedCurrency(_dbContext);
            SeedControlAccounts(_dbContext);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Add code here to clean up after seeding is complete
        return Task.CompletedTask;
    }

    private void SeedControlAccounts(ChevronCoopDbContext dbContext)
    {

        logger.LogInformation("starting SeedControlAccounts");

        var controlAcctNames = Enum.GetNames(typeof(ControlAccounts)).ToList();
        var currency = dbContext.Currencies.FirstOrDefault(x => x.Code.ToLower() == "ngn");

        if (currency == null)
        {
            currency = SeedCurrency(dbContext);
        }

        foreach (var controlAcctName in controlAcctNames)
        {

            var check = dbContext.LedgerAccounts.Any(p => p.Code == controlAcctName);

            if (!check)
            {

                logger.LogInformation($"Ledger account {controlAcctName} not found");


                LedgerAccount account = new LedgerAccount
                {
                    Code = controlAcctName,
                    Name = controlAcctName,
                    IsOfficeAccount = true,
                    AccountType = COAType.CONTROL,
                    AllowManualEntry = true,
                    CurrencyId = currency.Id
                };

                dbContext.LedgerAccounts.Add(account);
            }

        }

    }


    private Currency SeedCurrency(ChevronCoopDbContext dbContext)
    {

        logger.LogInformation("starting SeedCurrency");

        var currency = new Currency();
        var check = dbContext.Currencies.Any(p => p.Code.ToLower() == "ngn" || p.Name.ToLower().Contains("naira") || p.Name.ToLower().Contains("nigeria"));
        if (!check)
        {
            logger.LogInformation("NGN ccy not found");


            currency = new Currency
            {
                Code = "NGN",
                Description = "Naira",
                Name = "Naira",
                Symbol = "₦",
                IsoSymbol = "NGN"
            };

            dbContext.Currencies.Add(currency);
        }
        return currency;
    }





    private List<Permission> SetupPermissions(List<string> existingPermissionCodes)
    {
        var permissionCodes = typeof(PermissionConfig).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
          .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
          .Select(x => x.GetRawConstantValue())
          .ToHashSet();

        // filter existing permissions
        permissionCodes.ExceptWith(existingPermissionCodes);


        return (from string permissionCode in permissionCodes
                let permission = permissionCode.Split('.')
                where permission.Length >= 2
                select new Permission
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    Code = permissionCode,
                    Name = permissionCode.Replace('.', ' '),
                    Module = permission[0],
                    Category = permission[1]
                }).ToList();
    }

    private List<ApplicationRole> SetupRoles(List<string> existingRolesCode)
    {
        var roles = new List<ApplicationRole>();

        if (!existingRolesCode.Contains("SuperAdmin"))
        {
            roles.Add(new ApplicationRole
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                Code = "SuperAdmin",
                Name = "SuperAdmin",
                IsSystemRole = true
            });
        }

        if (!existingRolesCode.Contains("Admin"))
        {
            roles.Add(new ApplicationRole
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                Code = "Admin",
                Name = "Admin",
                IsSystemRole = true,
            });
        }

        if (!existingRolesCode.Contains("InternalControl"))
        {
            roles.Add(new ApplicationRole
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                Code = "InternalControl",
                Name = "Internal Control",
                IsSystemRole = true,
            });
        }

        if (!existingRolesCode.Contains("Regular"))
        {
            roles.Add(new ApplicationRole
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                Code = "Regular",
                Name = "Regular",
                IsSystemRole = true,
            });
        }

        if (!existingRolesCode.Contains("Retiree"))
        {
            roles.Add(new ApplicationRole
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                Code = "Retiree",
                Name = "Retiree",
                IsSystemRole = true,
            });
        }

        if (!existingRolesCode.Contains("Expatriate"))
        {
            roles.Add(new ApplicationRole
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                Code = "Expatriate",
                Name = "Expatriate",
                IsSystemRole = true,
            });
        }

        return roles;
    }

    private async Task<List<ApplicationRoleClaim>> SetupSuperAdminRoleClaims(ChevronCoopDbContext _dbContext)
    {
        var roleClaims = new List<ApplicationRoleClaim>();
        var permissionQuery = _dbContext.Permissions;
        var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Code.ToLower() == "superadmin");
        var existingRoleClaims = _dbContext.ApplicationRoleClaims.Where(x => x.RoleId == role.Id).Select(x => x.PermissionId).ToHashSet();

        var permissionIds = permissionQuery.Select(x => x.Id).ToHashSet();
        permissionIds.ExceptWith(existingRoleClaims);

        if (permissionIds.Any())
        {
            var permissions = await permissionQuery.Where(x => permissionIds.Contains(x.Id)).ToListAsync();
            foreach (var permission in permissions)
            {
                roleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = "Permission",
                    PermissionId = permission.Id,
                    ClaimValue = permission.Code
                });
            }
        }

        return roleClaims;
    }


    private async Task SetupSuperAdmin(ChevronCoopDbContext _dbContext, UserManager<ApplicationUser> _userManager, CoreAppSettings _appsettings)
    {
        try
        {
            var superAdminEmail = _appsettings.SuperAdminEmail;
            var userByEmailCheck = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == superAdminEmail);
            if (userByEmailCheck == null)
            {
                var department = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Name.ToLower() == "admin");
                if (department == null)
                {
                    department = new Department
                    {
                        Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                        Name = "Admin",
                        Description = "Admin",
                        IsActive = true
                    };

                    _dbContext.Departments.Add(department);
                }


                var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Code.ToLower() == "superadmin");

                var user = new ApplicationUser
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    NormalizedEmail = superAdminEmail,
                    NormalizedUserName = superAdminEmail,
                    EmailConfirmed = false,
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    PhoneNumber = "08123456789",
                    IsAdmin = true
                };

                var userPassword = _appsettings.SuperAdminPassword;
                var createUserResponse = await _userManager.CreateAsync(user, userPassword);
                if (createUserResponse.Succeeded)
                {
                    var member = new MemberProfile
                    {
                        ApplicationUserId = user.Id,
                        FirstName = "Super",
                        MiddleName = "",
                        LastName = "Admin",
                        Address = "Chevron coop",
                        PrimaryEmail = superAdminEmail,
                        MembershipId = "1234567890",
                        CAI = "1234567890",
                        Gender = Gender.UNKNOWN,
                        DepartmentId = department.Id,
                        Status = MemberProfileStatus.ACTIVE,
                        MemberType = MemberType.ADMIN
                    };

                    // Create member profile
                    await _dbContext.MemberProfiles.AddAsync(member);

                    // Add User Roles
                    await _dbContext.ApplicationUserRoles.AddRangeAsync(new ApplicationUserRole
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    });
                }

                // await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}