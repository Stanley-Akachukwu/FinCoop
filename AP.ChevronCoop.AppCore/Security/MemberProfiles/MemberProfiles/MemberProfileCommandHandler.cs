using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.MasterData.Country;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using Audit.Core;
using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Net;
using System.Net;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles
{
    public class MemberProfileCommandHandler :
     IRequestHandler<QueryMemberProfileCommand, CommandResult<IQueryable<MemberProfile>>>,
    IRequestHandler<CreateMemberProfileCommand, CommandResult<MemberProfileViewModel>>,
    IRequestHandler<UpdateMemberProfileCommand, CommandResult<MemberProfileViewModel>>,
    IRequestHandler<ApproveKYCCommand, CommandResult<MemberProfileViewModel>>,
    IRequestHandler<RegisterMemberCommand, CommandResult<RegisterMemberViewModel>>,
     IRequestHandler<CreateRetireeSwitchCommand, CommandResult<MemberProfileViewModel>>,
    IRequestHandler<VerifyMemberProfileCommand, CommandResult<string>>,
    IRequestHandler<DeleteMemberProfileCommand, CommandResult<string>>,
    IRequestHandler<UpdateMemberRegistrationCommand, CommandResult<RegisterMemberViewModel>>

    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoggerService _logger;
        private readonly IMapper mapper;
        private readonly IEmailService _emailService;
        private readonly CoreAppSettings _options;
        private readonly IManageApprovalService _approvalLog;

       

        public MemberProfileCommandHandler(ChevronCoopDbContext appDbContext, IMediator mediator, UserManager<ApplicationUser> userManager,
        ILoggerService logger, IMapper _mapper, IOptions<CoreAppSettings> options, IEmailService emailService, IManageApprovalService approvalLog)
        {
            dbContext = appDbContext;
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
            mapper = _mapper;
            _emailService = emailService;
            _options = options.Value;
            _approvalLog = approvalLog;
        }


        public Task<CommandResult<IQueryable<MemberProfile>>> Handle(QueryMemberProfileCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<IQueryable<MemberProfile>>();
            rsp.Response = dbContext.MemberProfiles;
            return Task.FromResult(rsp);
        }


        public async Task<CommandResult<MemberProfileViewModel>> Handle(CreateMemberProfileCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<MemberProfileViewModel>();


            using (var audit = await AuditScope.CreateAsync("MemberProfile:Get", () => rsp))
            {
                var entity = mapper.Map<MemberProfile>(request);

                Enum.TryParse<Gender>(request.Gender, out Gender gender);
                entity.Gender = gender;

                dbContext.MemberProfiles.Add(entity);
                await dbContext.SaveChangesAsync(cancellationToken);

                rsp.Response = mapper.Map<MemberProfileViewModel>(entity);

                audit.Comment("Create member profile");
                await audit.SaveAsync();
            }
            return rsp;
        }


        public async Task<CommandResult<RegisterMemberViewModel>> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<RegisterMemberViewModel>();
            try
            {
                var userByEmailCheck = await _userManager.FindByEmailAsync(request.Email);
                if (userByEmailCheck != null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = $"Email address  {request.Email} has been taken. Request not processed.";
                    return rsp;
                }

                var user = new ApplicationUser()
                {
                    Id = SequentialGuid.Create(SequentialGuidType.SequentialAsUlid).ToString(),
                    NormalizedEmail = request.Email,
                    NormalizedUserName = request.Email,
                    EmailConfirmed = false,
                    UserName = request.Email,
                    Email = request.Email
                };

                var createUserResponse = await _userManager.CreateAsync(user, request.Password);
                if (!createUserResponse.Succeeded)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = createUserResponse.Errors.FirstOrDefault()?.Description;
                    return rsp;
                }

                var member = new MemberProfile()
                {
                    ApplicationUserId = user.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    ResidentialAddress = request.Location,
                    PrimaryEmail = request.Email,
                    Address = request.Location,
                    Country = request.Location,
                    Status = MemberProfileStatus.PENDING_APPROVAL,
                    Gender = Gender.UNKNOWN,
                    MembershipId = request.MembershipId,
                    CAI = request.MembershipId,
                    IsKycStarted = request.IsKycStarted,
                    KycStartDate = DateTime.Now,
                    MemberType = request.Role
                };

                // Create member profile
                await dbContext.MemberProfiles.AddAsync(member, cancellationToken);


                // Create approval
                // var approvalRequest = new CreateApprovalModel()
                // {
                //     Module = "MemberProfile",
                //     Payload = JsonConvert.SerializeObject(request),
                //     Comment = "Member Profile  approval initiated",
                //     ApprovalType = ApprovalType.MEMBER_ENROLLMENT,
                //     Description = $"Member Registration - {member.FirstName} {member.LastName} ({member.MembershipId})",
                //     CreatedBy = member.Id,
                //     EntityId = member.Id,
                //     EntityType = typeof(RegisterMemberCommand)
                // };
                //
                // var approval = await _approvalLog.CreateApproval(approvalRequest, true);
                
                // Manage User Role
                // Add User Roles
                if (!String.IsNullOrEmpty(request.Role.ToString()))
                {
                    var role = await dbContext.ApplicationRoles.FirstOrDefaultAsync(x => x.Code.ToLower() == request.Role.ToString().ToLower(), cancellationToken: cancellationToken);

                    if (role is not null)
                    {
                        dbContext.ApplicationUserRoles.Add(new ApplicationUserRole()
                        {
                            RoleId = role.Id,
                            UserId = user.Id
                        });
                    }
                }

                await dbContext.SaveChangesAsync(cancellationToken);
               
                // send mail to user
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var props = new GeneralEmailDto
                {
                    Link = $"{_options.WebBaseUrl}/emailVerified?email={user.Email}&token={code}",
                    Name = member.FirstName
                };

                _ = _emailService.SendEmailAsync(EmailTemplateType.ENROLLMENT, user.Email, props);

                rsp.StatusCode = (int)HttpStatusCode.OK;
                // rsp.Response = new RegisterMemberViewModel { ApprovalId = approval.Response?.Id, UserId = user.Id, MemberId = member.Id };
                rsp.Response = new RegisterMemberViewModel { UserId = user.Id, MemberId = member.Id };

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = $"{ex.Message}. Your request could not be completed.Contact the admin.";
                _logger.LogError(nameof(MemberProfileCommandHandler), nameof(RegisterMemberCommand), ex);
                return rsp;
            }
        }

        public async Task<CommandResult<RegisterMemberViewModel>> Handle(UpdateMemberRegistrationCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<RegisterMemberViewModel>();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = $"Application User Record with  {request.Email} was not found.Update Operation aborted";
                    return rsp;
                }

                user.NormalizedEmail = request.Email;
                user.NormalizedUserName = request.Email;
                user.EmailConfirmed = false;
                user.UserName = request.Email;
                user.Email = request.Email;

                
 
                var UpdateUserResponse = await _userManager.UpdateAsync(user);



              var memberProfile = new MemberProfile();
               memberProfile =  await dbContext.MemberProfiles.Where(u=>u.PrimaryEmail==user.Email && u.MembershipId==request.MembershipId).FirstOrDefaultAsync();
                if(memberProfile == null)
                {
                    memberProfile = new MemberProfile()
                    {
                        ApplicationUserId = user.Id,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        ResidentialAddress = request.Location,
                        PrimaryEmail = request.Email,
                        Address = request.Location,
                        Country = request.Location,
                        Status = MemberProfileStatus.PENDING_APPROVAL,
                        Gender = Gender.UNKNOWN,
                        MembershipId = request.MembershipId,
                        CAI = request.MembershipId,
                        IsKycStarted = request.IsKycStarted,
                        KycStartDate = DateTime.Now,
                        MemberType = request.Role
                    };
                    await dbContext.MemberProfiles.AddAsync(memberProfile, cancellationToken);
                }
                else
                {
                    memberProfile.ApplicationUserId = user.Id;
                    memberProfile.FirstName = request.FirstName;
                    memberProfile.LastName = request.LastName;
                    memberProfile.ResidentialAddress = request.Location;
                    memberProfile.PrimaryEmail = request.Email;
                    memberProfile.Address = request.Location;
                    memberProfile.Country = request.Location;
                    memberProfile.Status = MemberProfileStatus.PENDING_APPROVAL;
                    memberProfile.Gender = Gender.UNKNOWN;
                    memberProfile.MembershipId = request.MembershipId;
                    memberProfile.CAI = request.MembershipId;
                    memberProfile.IsKycStarted = request.IsKycStarted;
                    memberProfile.KycStartDate = DateTime.Now;
                    memberProfile.MemberType = request.Role;
                     dbContext.MemberProfiles.Update(memberProfile);
                }
                 



                // Add User Roles
                if (!String.IsNullOrEmpty(request.Role.ToString()))
                {
                    var role = await dbContext.ApplicationRoles.FirstOrDefaultAsync(x => x.Code.ToLower() == request.Role.ToString().ToLower(), cancellationToken: cancellationToken);

                    if (role is not null)
                    {
                       var userRoleExist = dbContext.ApplicationUserRoles.Where(r=>r.UserId==user.Id).Any();
                        if (!userRoleExist)
                            dbContext.ApplicationUserRoles.Add(new ApplicationUserRole()
                            {
                                RoleId = role.Id,
                                UserId = user.Id
                            });


                    }
                }

                await dbContext.SaveChangesAsync(cancellationToken);

                // send mail to user
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var props = new GeneralEmailDto
                {
                    Link = $"{_options.WebBaseUrl}/emailVerified?email={user.Email}&token={code}",
                    Name = memberProfile.FirstName
                };

                _ = _emailService.SendEmailAsync(EmailTemplateType.ENROLLMENT, user.Email, props);

                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Response = new RegisterMemberViewModel { UserId = user.Id, MemberId = memberProfile.Id };

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = $"{ex.Message}. Your request could not be completed.Contact the admin.";
                _logger.LogError(nameof(MemberProfileCommandHandler), nameof(RegisterMemberCommand), ex);
                return rsp;
            }
        }
        public async Task<CommandResult<string>> Handle(VerifyMemberProfileCommand request, CancellationToken cancellationToken)
        {

            var rsp = new CommandResult<string>();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "User not found";
                return rsp;
            }

            var verifyUser = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!verifyUser.Succeeded)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = verifyUser.Errors.FirstOrDefault()?.Description;
                return rsp;
            }

            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Response = "User account verified!";
            return rsp;
        }

        public async Task<CommandResult<MemberProfileViewModel>> Handle(UpdateMemberProfileCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<MemberProfileViewModel>();
            var entity = await dbContext.MemberProfiles.FindAsync(request.Id);

            if (entity is null)
            {
                rsp.ErrorFlag = true;
                rsp.Message = "Member profile not found.";
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                return rsp;
            }

            mapper.Map(request, entity);
            entity.DateOfEmployment = request.DateOfEmployment;
            entity.YearsOfService = request.YearsOfService;
            // entity.DepartmentId = request.DepartmentId;
            
            if (request.SubmitKyc)
            {
                // Verify required properties
                if (
                    string.IsNullOrEmpty(entity.PrimaryEmail) ||
                    // string.IsNullOrEmpty(entity.SecondaryEmail) ||
                    string.IsNullOrEmpty(entity.PrimaryPhone) ||
                    string.IsNullOrEmpty(entity.FirstName) ||
                    string.IsNullOrEmpty(entity.LastName) ||
                    string.IsNullOrEmpty(entity.MembershipId) ||
                    // string.IsNullOrEmpty(entity.JobRole) ||
                    string.IsNullOrEmpty(entity.ApplicationUserId)
                    // string.IsNullOrEmpty(entity.DepartmentId)
                )
                {
                    rsp.ErrorFlag = true;
                    rsp.Message = "Member bio data not completed";
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    return rsp;
                }

                var checkNextOfKin = dbContext.MemberNextOfKins.Any(x => x.ProfileId == entity.Id);
                if (!checkNextOfKin)
                {
                    rsp.ErrorFlag = true;
                    rsp.Message = "Next of kin is required";
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    return rsp;
                }

                var checkAccountDetails = dbContext.MemberBankAccounts.Any(x => x.ProfileId == entity.Id);
                if (!checkAccountDetails)
                {
                    rsp.ErrorFlag = true;
                    rsp.Message = "Account details is required";
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    return rsp;
                }

                entity.KycSubmitted = true;
                entity.KycSubmittedOn = DateTime.Now;
                entity.Status = MemberProfileStatus.AWAITING_KYC_APPROVAL;
            }

            dbContext.MemberProfiles.Update(entity);

            // Update customer
            var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.ApplicationUserId == entity.Id, cancellationToken: cancellationToken);
            if (customer is not null)
            {
                mapper.Map(entity, customer);
                dbContext.Customers.Update(customer);
            }
            
            
            if (request.SubmitKyc)
            {
                request.IdentificationUrl = "";
                request.PassportUrl = "";
                request.ProfileImageUrl = "";
                
                // Log KYC information
               
                var approvalRequest = new CreateApprovalModel
                {
                    Module = "MemberProfile",
                    Payload =System.Text.Json.JsonSerializer.Serialize(request),
                    Comment = "KYC record updated",
                    ApprovalType = ApprovalType.KYC_COMPLETION,
                    Description = $"KYC Approval - {entity.FirstName} {entity.LastName} ({entity.MembershipId})",
                    CreatedBy = request.ApplicationUserId,
                    EntityId = entity.Id,
                    EntityType = typeof(UpdateMemberProfileCommand)
                };
                    
                await _approvalLog.CreateApproval(approvalRequest, true);
            }
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            rsp.Response = mapper.Map<MemberProfileViewModel>(entity);
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Message = "Cooperate member information updated successfully.";
            return rsp;
        }

        public async Task<CommandResult<MemberProfileViewModel>> Handle(ApproveKYCCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<MemberProfileViewModel>();
            var user = await _userManager.FindByIdAsync(request.ApprovedBy);
            if (user == null)
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "You are not in role to process this  approval request.";
                return rsp;
            }

            if (!await _userManager.IsInRoleAsync(user, "Internal Control"))
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "You do not have the permission for this request.";
                return rsp;
            }

            var entity = await dbContext.MemberProfiles.FindAsync(request.MemberProfileId);
            if (entity == null)
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "No member profile record could be associated with this request.";
                return rsp;
            }

            var approval = await dbContext.Approvals.FirstOrDefaultAsync(p => p.Id == request.ApprovalId, cancellationToken: cancellationToken);
            if (approval == null)
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "No approval setup was found for this request.";
                return rsp;
            }

            var updateApprovalCommand = mapper.Map<UpdateApprovalCommand>(approval);
            updateApprovalCommand.UpdatedByUserId = updateApprovalCommand.CreatedByUserId;
            updateApprovalCommand.DateUpdated = DateTime.Now;
            var approvalResponse = await _mediator.Send(updateApprovalCommand, cancellationToken);
            if (approvalResponse.Response == null)
            {
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = "Approval update was not processed successfully for this request. Please contact the admin.";
                return rsp;
            }

            ApprovalViewModel approvalVM = approvalResponse.Response;

            if (approvalVM.IsApprovalCompleted)
            {
                entity.Status = MemberProfileStatus.ACTIVE;
                rsp.Message = "Member profile approved";
            }

            dbContext.MemberProfiles.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            rsp.Response = mapper.Map<MemberProfileViewModel>(entity);
            return rsp;
        }

        public async Task<CommandResult<string>> Handle(DeleteMemberProfileCommand request, CancellationToken cancellationToken)
        {
            // var rsp = await mediator.Send(model);
            var rsp = new CommandResult<string>();
            var memberProfile = await dbContext.MemberProfiles.Where(x => x.Id == request.Id)
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (memberProfile is null)
            {
                rsp.ErrorFlag = true;
                rsp.Message = "User not found";
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                return rsp;
            }

            memberProfile.IsDeleted = true;
            memberProfile.IsActive = true;
            
            dbContext.MemberProfiles.Update(memberProfile);
            await dbContext.SaveChangesAsync(cancellationToken);

            rsp.ErrorFlag = false;
            rsp.Message = "User deleted";
            rsp.StatusCode = (int)HttpStatusCode.OK;
            return rsp;
        }

        public async Task<CommandResult<MemberProfileViewModel>> Handle(CreateRetireeSwitchCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<MemberProfileViewModel>();
            var entity = await dbContext.MemberProfiles.Where(x => x.Id == request.MemberProfileId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);


            var approvalRequest = new CreateApprovalModel
            {
                Module = "MemberProfile",
                Payload = JsonConvert.SerializeObject(request),
                Comment = "Retiree Switch Initiated",
                ApprovalType = ApprovalType.RETIREE_SWITCH,
                Description = $"Retiree Switch - {entity.FirstName} {entity.LastName} ({entity.MembershipId})",
                CreatedBy = request.InitiatedBy,
                EntityId = request.MemberProfileId,
                EntityType = typeof(CreateRetireeSwitchCommand)
            };
                    
            await _approvalLog.CreateApproval(approvalRequest, true);

            rsp.ErrorFlag = false;
            rsp.Message = "User deleted";
            rsp.StatusCode = (int)HttpStatusCode.OK;
            return rsp;
        }
    }
}
