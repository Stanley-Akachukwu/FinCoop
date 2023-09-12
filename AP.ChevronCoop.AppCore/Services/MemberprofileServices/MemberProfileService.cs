using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Services.MemberprofileServices
{
    public class MemberProfileService : IMemberProfileService
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly ILogger<MemberProfileService> _logger;



        public MemberProfileService(ChevronCoopDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
             IMapper mapper, IMediator mediator, IEmailService emailService, ILogger<MemberProfileService> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _mediator = mediator;
            _emailService = emailService;
            _logger = logger;

        }
        public async Task<CommandResult<string>> RegisterMigratedMember(MemberBulkUploadTemp request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var userByEmailCheck = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmailCheck != null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = $"Email address  {request.Email} has been taken. Request not processed.";
                return rsp;
            }

            var password = StringHelper.GenerateRandomString(10);
            try
            {
                var user = new ApplicationUser()
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    NormalizedEmail = request.Email,
                    NormalizedUserName = request.Email,
                    EmailConfirmed = false,
                    UserName = request.Email,
                    Email = request.Email
                };

                var createUserResponse = await _userManager.CreateAsync(user, password);
                if (!createUserResponse.Succeeded)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = createUserResponse.Errors.FirstOrDefault()?.Description;
                    return rsp;
                }

                Gender gender = Gender.UNKNOWN;

                switch (request.Gender)
                {
                    case nameof(Gender.MALE):
                        gender = Gender.MALE;
                        break;
                    case nameof(Gender.FEMALE):
                        gender = Gender.FEMALE;
                        break;
                    default:
                        gender = Gender.UNKNOWN;
                        break;
                }


                var member = new MemberProfile()
                {
                    ApplicationUserId = user.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    ResidentialAddress = request.Country,
                    PrimaryEmail = request.Email,
                    Address = request.Country,
                    Country = request.Country,
                    Status = MemberProfileStatus.ACTIVE,
                    Gender = gender,
                    MembershipId = request.MembershipNumber,
                    IsKycStarted = true,
                    KycStartDate = DateTime.Now,
                    IsKycCompleted = true,
                    KycCompletedDate = DateTime.Now
                };

                if (String.Equals(request.UserRole, MemberType.RETIREE.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    member.RetireeNumber = request.MembershipNumber.Remove(0, 1).Insert(0, "R");
                else
                    member.CAI = request.MembershipNumber;


                await _dbContext.MemberProfiles.AddAsync(member, cancellationToken);

                if (!String.IsNullOrEmpty(request.UserRole))
                {
                    var role = await _dbContext.ApplicationRoles.FirstOrDefaultAsync(x => x.Name.ToLower() == request.UserRole.ToLower());

                    if (role is not null)
                    {
                        _dbContext.ApplicationUserRoles.Add(new ApplicationUserRole()
                        {
                            RoleId = role.Id,
                            UserId = user.Id
                        });
                    }
                }
                await _dbContext.SaveChangesAsync(cancellationToken);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var emailBody = $@"
                    <html>
                        <body>
                            <p>Kindly use the temporal password below to login to Chevron Coop portal. </p>
                            <p> temp password :{password}</p>
                        </body>
                    </html>
                ";

                var message = new Message(user.Email, "CEMCS Email Verification", emailBody);
                await _emailService.SendEmailAsync(message);

                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Response = "Successful.";
                return rsp;

            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(MemberProfileService), nameof(RegisterMigratedMember), ex);
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.InternalServerError;
                rsp.Message = ex.Message + ". Your request could not be completed.Contact the admin.";
                return rsp;
            }

        }

    }
}
