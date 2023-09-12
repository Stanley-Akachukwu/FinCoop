using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUsers
{
    public class ApplicationUserCommandHandler :
    IRequestHandler<QueryApplicationUserCommand, CommandResult<IQueryable<ApplicationUser>>>,
    IRequestHandler<CreateApplicationUserCommand, CommandResult<ApplicationUserViewModel>>,
    IRequestHandler<UpdateApplicationUserCommand, CommandResult<ApplicationUserViewModel>>,
    IRequestHandler<UpdateUserStatusCommand, CommandResult<string>>,
    IRequestHandler<DeleteApplicationUserCommand, CommandResult<string>>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly CoreAppSettings _options;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<ApplicationUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public ApplicationUserCommandHandler(
            ChevronCoopDbContext appDbContext, IOptions<CoreAppSettings> options,
            UserManager<ApplicationUser> userManager, IEmailService emailService,
            ILogger<ApplicationUserCommandHandler> logger, IMapper mapper
        )
        {
            dbContext = appDbContext;
            _options = options.Value;
            _userManager = userManager;
            _emailService = emailService;
            _logger = logger;
            _mapper = mapper;
        }


        public Task<CommandResult<IQueryable<ApplicationUser>>> Handle(QueryApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<ApplicationUser>>();
            rsp.Response = dbContext.ApplicationUsers;
            return Task.FromResult(rsp);
        }

        public async Task<CommandResult<ApplicationUserViewModel>> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ApplicationUserViewModel>();

            var userByEmailCheck = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmailCheck != null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = $"Email address {request.Email} has been taken. Request not processed.";
                return rsp;
            }

            var department = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken: cancellationToken);

            if (department == null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Selected department not found";
                return rsp;
            }

            var roles = await dbContext.Roles.Where(x => request.RoleIds.Contains(x.Id))
                .ToListAsync(cancellationToken: cancellationToken);

            if (!roles.Any())
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Minimum of a role should be selected";
                return rsp;
            }

            var user = new ApplicationUser
            {
                Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                NormalizedEmail = request.Email,
                NormalizedUserName = request.Email,
                EmailConfirmed = false,
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsAdmin = true
            };

            var userPassword = StringHelper.GenerateRandomString(10);
            var createUserResponse = await _userManager.CreateAsync(user, userPassword);
            if (!createUserResponse.Succeeded)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = createUserResponse.Errors.FirstOrDefault()?.Description;
                return rsp;
            }

            Enum.TryParse(request.Gender, out Gender _gender);
            Enum.TryParse(request.Status, out MemberProfileStatus _status);
            var member = new MemberProfile
            {
                ApplicationUserId = user.Id,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Address = request.Address,
                PrimaryEmail = request.Email,
                MembershipId = request.MembershipId,
                CAI = request.MembershipId,
                Gender = _gender,
                DepartmentId = request.DepartmentId,
                Status = _status,
                Country = request.Country,
                State = request.State,
            };

            // Create member profile
            await dbContext.MemberProfiles.AddAsync(member, cancellationToken);

            // Add User Roles
            var userRoles = new List<ApplicationUserRole>();
            foreach (var role in roles)
            {
                userRoles.Add(new ApplicationUserRole
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }

            await dbContext.ApplicationUserRoles.AddRangeAsync(userRoles, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ApplicationUserViewModel>(user);
            response.FirstName = request.FirstName;
            response.LastName = request.LastName;
            response.MiddleName = request.MiddleName;

            var props = new CreateApplicationUserEmailDto()
            {
                Link = _options.WebBaseUrl,
                Password = userPassword,
                Name = member.FirstName
            };
            _ = _emailService.SendEmailAsync(EmailTemplateType.CREATE_APPLICATION_USER, request.Email, props);

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = response;
            return rsp;
        }

        public async Task<CommandResult<ApplicationUserViewModel>> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ApplicationUserViewModel>();
            try
            {
                var department = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken: cancellationToken);

                if (department == null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Selected department not found";
                    return rsp;
                }

                var user = await dbContext.MemberProfiles
                    .Include(x => x.ApplicationUser)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                if (user is null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "User profile not found";
                    return rsp;
                }

                Enum.TryParse(request.Gender, out Gender _gender);
                Enum.TryParse(request.Status, out MemberProfileStatus _status);

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.MiddleName = request.MiddleName;
                user.Address = request.Address;
                user.CAI = request.MembershipId;
                user.MembershipId = request.MembershipId;
                user.Gender = _gender;
                user.DepartmentId = request.DepartmentId;
                user.Status = _status;
                user.Country = request.Country;
                user.State = request.State;
                user.ApplicationUser.PhoneNumber = request.PhoneNumber;

                // Update member profile
                dbContext.MemberProfiles.Update(user);

                await dbContext.SaveChangesAsync(cancellationToken);


                var response = _mapper.Map<ApplicationUserViewModel>(user);
                response.FirstName = request.FirstName;
                response.LastName = request.LastName;
                response.MiddleName = request.MiddleName;

                rsp.Response = response;
                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Message = "Record updated successfully.";
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

        public async Task<CommandResult<string>> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (user is null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Response = "User not found";
                return rsp;
            }

            dbContext.ApplicationUsers.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = "Data successfully deleted";
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            var user = await dbContext.MemberProfiles.FirstOrDefaultAsync(x => x.ApplicationUserId == request.UserId, cancellationToken: cancellationToken);
            if (user is null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Member profile not found";
                return rsp;
            }
            Enum.TryParse(request.Status, out MemberProfileStatus _status);
            user.Status = _status;

            var _member = dbContext.MemberProfiles.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = "User status updated";
            return rsp;
        }
    }
}
