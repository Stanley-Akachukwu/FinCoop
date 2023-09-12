using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{
    public class ApplicationUserLoginCommandHandler :
     IRequestHandler<QueryApplicationUserLoginCommand, CommandResult<IQueryable<ApplicationUserLogin>>>,
    IRequestHandler<CreateApplicationUserLoginCommand, CommandResult<ApplicationUserLoginViewModel>>,
    IRequestHandler<UpdateApplicationUserLoginCommand, CommandResult<ApplicationUserLoginViewModel>>,
    IRequestHandler<LoginCommand, CommandResult<LoginViewModel>>,
    IRequestHandler<SwitchAccountCommand, CommandResult<LoginViewModel>>,
    IRequestHandler<DeleteApplicationUserLoginCommand, CommandResult<string>>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ApplicationUserLoginCommandHandler(ChevronCoopDbContext appDbContext, UserManager<ApplicationUser> userManager,
        ILogger<ApplicationUserLoginCommandHandler> _logger, IMapper _mapper)
        {

            dbContext = appDbContext;
            _userManager = userManager;
            logger = _logger;
            mapper = _mapper;

        }


        public Task<CommandResult<IQueryable<ApplicationUserLogin>>> Handle(QueryApplicationUserLoginCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<IQueryable<ApplicationUserLogin>>();
            rsp.Response = dbContext.ApplicationUserLogins;

            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<ApplicationUserLoginViewModel>> Handle(CreateApplicationUserLoginCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationUserLoginViewModel>();
            var entity = mapper.Map<ApplicationUserLogin>(request);

            dbContext.ApplicationUserLogins.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<ApplicationUserLoginViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<LoginViewModel>();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null || user.PasswordHash is null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Invalid login credential";
                    return rsp;
                }

                var verification = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (verification != PasswordVerificationResult.Success)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Invalid login credential";
                    return rsp;
                }

                var getUserProfile =
                    await dbContext.MemberProfiles
                        .FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id, cancellationToken: cancellationToken);

                if (getUserProfile is null ||
                    getUserProfile.Status == MemberProfileStatus.PENDING_APPROVAL ||
                    getUserProfile.Status == MemberProfileStatus.SUSPENDED ||
                    getUserProfile.Status == MemberProfileStatus.DEACTIVATED)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Inactive account, contact the administrator";
                    return rsp;
                }

                var response = new LoginViewModel
                {
                    Email = user.Email,
                    Id = user.Id,
                    FirstName = getUserProfile.FirstName,
                    LastName = getUserProfile.LastName,
                    MiddleName = getUserProfile.MiddleName,
                    MembershipId = getUserProfile.MembershipId,
                    MembershipType = getUserProfile.MemberType.ToString(),
                    IsAdmin = user.IsAdmin,
                    // ProfilePicture = getUserProfile.ProfileImageUrl,

                    // Validate User KYC Status
                    KycStatus = IsKYCCompleted(getUserProfile)
                };


                var claims = new List<LoginClaim>();
                var roles = new List<RoleLookup>();

                // Get user role claims
                var _userRoles = await dbContext.ApplicationUserRoles.Where(x => x.UserId == user.Id)
                    .ToListAsync(cancellationToken: cancellationToken);

                if (_userRoles.Any())
                {
                    var roleIds = _userRoles.Select(x => x.RoleId).ToList();

                    var _roles = await dbContext.ApplicationRoles.Where(x => roleIds.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);
                    roles.AddRange(_roles.Select(x => new RoleLookup()
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name
                    }));

                    
                    HashSet<string> roleHash = new HashSet<string>(roles.Select(x => x.Code.ToLower()).ToList());

                    var corporateRoles = new HashSet<string>(new string[] { "regular", "expatriate", "retiree" });

                    // Corporate user only checks
                    if (roles.Count == 1 && roleHash.Any(x => corporateRoles.Contains(x)))
                    {
                        if (getUserProfile.Status == MemberProfileStatus.AWAITING_KYC_APPROVAL)
                        {
                            rsp.ErrorFlag = true;
                            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                            rsp.Message = "Account awaiting KYC Approval, contact the administrator";
                            return rsp;
                        }

                        if (getUserProfile.Status != MemberProfileStatus.ACTIVE && getUserProfile.Status != MemberProfileStatus.AWAITING_KYC_COMPLETION)
                        {
                            rsp.ErrorFlag = true;
                            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                            rsp.Message = "Inactive account, contact the administrator";
                            return rsp;
                        }
                        
                        var customer =
                            await dbContext.Customers
                                .FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id, cancellationToken: cancellationToken);
                
                        response.CustomerId = customer?.Id;

                        response.IsAdmin = false;
                    }

                    if (roles.Count > 1)
                    {
                        roleHash.ExceptWith(corporateRoles);
                        if (roleHash.Any())
                        {
                            response.IsAdmin = true;
                        }
                        roleIds = _roles.Where(x => x.Code != null && roleHash.Contains(x.Code.ToLower())).Select(x => x.Id).ToList();
                    }

                    var userRoleClaims = await dbContext.ApplicationRoleClaims.Where(x => roleIds.Contains(x.RoleId)).ToListAsync(cancellationToken: cancellationToken);

                    claims.AddRange(userRoleClaims.Select(claim => new LoginClaim
                    {
                        Id = claim.Id.ToString(),
                        Code = claim.ClaimValue
                    }));
                }
                
                response.Claims = claims;
                response.Roles = roles;

                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Response = response;
                return rsp;
            }
            catch (Exception e)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = e.Message;
                logger.LogError("{Exception}", JsonConvert.SerializeObject(e));
                return rsp;
            }
        }

        private bool IsKYCCompleted(MemberProfile profile)
        {
            if (
                string.IsNullOrEmpty(profile.PrimaryEmail) ||
                // string.IsNullOrEmpty(profile.SecondaryEmail) ||
                string.IsNullOrEmpty(profile.PrimaryPhone) ||
                string.IsNullOrEmpty(profile.FirstName) ||
                string.IsNullOrEmpty(profile.LastName) ||
                string.IsNullOrEmpty(profile.MembershipId) ||
                // string.IsNullOrEmpty(profile.JobRole) ||
                // string.IsNullOrEmpty(profile.DepartmentId) ||
                string.IsNullOrEmpty(profile.ApplicationUserId) 
            )
                return false;

            // Check Next of Kin
            var checkNextOfKin = dbContext.MemberNextOfKins.Any(x => x.ProfileId == profile.Id);
            var checkAccountDetails = dbContext.MemberBankAccounts.Any(x => x.ProfileId == profile.Id);

            return checkNextOfKin && checkAccountDetails;
        }


        public async Task<CommandResult<LoginViewModel>> Handle(SwitchAccountCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<LoginViewModel>();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                
                
                if (user is null || user.PasswordHash is null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Invalid login credential";
                    return rsp;
                }

                var getUserProfile =
                    await dbContext.MemberProfiles
                        .FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id, cancellationToken: cancellationToken);

                if (getUserProfile is null ||
                    getUserProfile.Status == MemberProfileStatus.PENDING_APPROVAL ||
                    getUserProfile.Status == MemberProfileStatus.SUSPENDED ||
                    getUserProfile.Status == MemberProfileStatus.DEACTIVATED)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Inactive account, contact the administrator";
                    return rsp;
                }

                var response = new LoginViewModel
                {
                    Email = user.Email,
                    Id = user.Id,
                    FirstName = getUserProfile.FirstName,
                    LastName = getUserProfile.LastName,
                    MiddleName = getUserProfile.MiddleName,
                    MembershipId = getUserProfile.MembershipId,
                    MembershipType = getUserProfile.MemberType.ToString(),
                    // IsAdmin = user.IsAdmin,
                    IsAdmin = !request.SwitchToCorporate,
                    // ProfilePicture = getUserProfile.ProfileImageUrl,

                    // Validate User KYC Status
                    KycStatus = IsKYCCompleted(getUserProfile)
                };


                var claims = new List<LoginClaim>();
                var roles = new List<RoleLookup>();

                // Get user role claims
                var _userRoles = await dbContext.ApplicationUserRoles.Where(x => x.UserId == user.Id)
                    .ToListAsync(cancellationToken: cancellationToken);

                
                if (_userRoles.Any())
                {
                    // _userRoles.Contains(x => x.co)
                    var roleIds = _userRoles.Select(x => x.RoleId).ToList();

                    var _roles = await dbContext.ApplicationRoles.Where(x => roleIds.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);
                    roles.AddRange(_roles.Select(x => new RoleLookup()
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name
                    }));

                    var roleHash = new HashSet<string>(roles.Select(x => x.Code).ToList());

                    var corporateRoles = new HashSet<string>(new string[]
                    {
                        MemberType.REGULAR.ToString().ToLower(), 
                        MemberType.EXPATRIATE.ToString().ToLower(), 
                        MemberType.RETIREE.ToString().ToLower(), 
                    });

                    // Corporate user only checks
                    if (roles.Count == 1 && roleHash.Any(x => corporateRoles.Contains(x.ToLower())))
                    {
                        if (getUserProfile.Status == MemberProfileStatus.AWAITING_KYC_APPROVAL)
                        {
                            rsp.ErrorFlag = true;
                            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                            rsp.Message = "Account awaiting KYC Approval, contact the administrator";
                            return rsp;
                        }

                        if (getUserProfile.Status != MemberProfileStatus.ACTIVE && getUserProfile.Status != MemberProfileStatus.AWAITING_KYC_COMPLETION)
                        {
                            rsp.ErrorFlag = true;
                            rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                            rsp.Message = "Inactive account, contact the administrator";
                            return rsp;
                        }
                    }

                    if (roles.Count > 1)
                    {
                        if (request.SwitchToCorporate)
                        {
                            var customer =
                                await dbContext.Customers
                                    .FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id, cancellationToken: cancellationToken);
                
                            response.CustomerId = customer?.Id;

                            roleHash.IntersectWith(corporateRoles);
                        }
                        else
                            roleHash.ExceptWith(corporateRoles);

                        roleIds = _roles.Where(x => x.Code != null && roleHash.Contains(x.Code)).Select(x => x.Id).ToList();
                    }

                    var userRoleClaims = await dbContext.ApplicationRoleClaims.Where(x => roleIds.Contains(x.RoleId)).ToListAsync(cancellationToken: cancellationToken);
                    claims.AddRange(userRoleClaims.Select(claim => new LoginClaim
                    {
                        Id = claim.Id.ToString(),
                        Code = claim.ClaimValue
                    }));
                }
                
                response.Claims = claims;
                response.Roles = roles;

                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Response = response;
                return rsp;
            }
            catch (Exception e)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = e.Message;
                return rsp;
            }
        }


        public async Task<CommandResult<ApplicationUserLoginViewModel>> Handle(UpdateApplicationUserLoginCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<ApplicationUserLoginViewModel>();
            var entity = await dbContext.ApplicationUserLogins.FindAsync(request.Id);

            mapper.Map(request, entity);

            dbContext.ApplicationUserLogins.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = mapper.Map<ApplicationUserLoginViewModel>(entity);

            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteApplicationUserLoginCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var entity = await dbContext.ApplicationUserLogins.FindAsync(request.Id);

            dbContext.ApplicationUserLogins.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.Response = "Data successfully deleted";

            return rsp;
        }
    }
}
