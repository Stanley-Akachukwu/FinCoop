using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBulkUploads
{
    public class MemberBulkUploadCommandHandler :
    IRequestHandler<CreateMemberBulkUploadCommand, CommandResult<MemberBulkUploadViewModel>>,
        IRequestHandler<ValidateMemberBulkUploadCommand, CommandResult<MemberBulkUploadViewModel>>,
          IRequestHandler<NotifyMemberUploadRegistrationCommand, CommandResult<bool>>,
        IRequestHandler<GetMemberBulkUploadTempCommand, CommandResult<List<MemberBulkUploadTemp>>>
      

    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly IManageApprovalService _approval;
        private readonly CoreAppSettings _options;

        public MemberBulkUploadCommandHandler(ChevronCoopDbContext dbContext,  IMapper mapper, IMediator mediator, IEmailService emailService,  IManageApprovalService approval, IOptions<CoreAppSettings> options)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mediator = mediator;
            _emailService = emailService;
            _approval = approval;
            _options = options.Value;
        }

        public async Task<CommandResult<List<MemberBulkUploadTemp>>> Handle(GetMemberBulkUploadTempCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<List<MemberBulkUploadTemp>>();

            var memberBulkUploads = await _dbContext.MemberBulkUploadTemp.Where(x => x.SessionId == request.SessionId).ToListAsync();

            rsp.Response = memberBulkUploads;
            rsp.StatusCode = (int)HttpStatusCode.OK;
            rsp.Message = "Temp data found.";
            return rsp;
        }
        public async Task<CommandResult<MemberBulkUploadViewModel>> Handle(ValidateMemberBulkUploadCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<MemberBulkUploadViewModel>();
            var RejectedMemberDataUpload = new List<MemberDataUpload>();
            var AcceptedMemberDataUpload = new List<MemberDataUpload>();
           // var SessionId = NHiloHelper.GetNextKey(nameof(MemberBulkUploadSession)).ToString();


            try
            {
                int recordId = 1;

                request.MemberDataUploads.ForEach(x =>
                {
                    x.RecordId = recordId++;
                    x.Messages = new List<ValidationMessage>();
                });
               
                rsp.Response = new MemberBulkUploadViewModel
                {
                    AcceptedMemberDataUpload = request.MemberDataUploads,
                    SessionId = request.SessionId,
                };
                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Message = "Data validation completed successfully.";

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = ex.Message + ". Data validation could not be completed.Contact the admin.";
               // await _logger.LogError(nameof(MemberBulkUploadCommandHandler), nameof(ValidateMemberBulkUploadCommand), ex);
                return rsp;
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{13})$").Success;
        }

        public async Task<CommandResult<MemberBulkUploadViewModel>> Handle(CreateMemberBulkUploadCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<MemberBulkUploadViewModel>();
            var RejectedMemberDataUpload = new List<MemberDataUpload>();
            var AcceptedMemberDataUpload = new List<MemberDataUpload>();
            var memberBulkUploadTemps = new List<MemberBulkUploadTemp>();
            var approvalId = string.Empty;

            try
            {

                int recordId = 1;
                foreach (var member in request.MemberDataUploads)
                {

                    var temp = new MemberBulkUploadTemp();

                    temp.Gender = member.Gender;
                    temp.MemberType = member.MemberType;
                    temp.FirstName = member.FirstName;
                    temp.LastName = member.LastName;
                    temp.PhoneNumber = member.PhoneNumber;
                    temp.MembershipNumber = member.MembershipNumber;
                    temp.Status = member.Status;
                    temp.Email = member.Email;
                    temp.PhoneNumber = member.PhoneNumber;
                    temp.PhoneNumber = member.PhoneNumber;
                    temp.RecordId = recordId++;
                    temp.UploadedByUserId = request.UploadedByUserId;
                    temp.SessionId = request.SessionId;
                    temp.ApprovalId = approvalId;
                    temp.Description = "Coop member bulk registration upload";
                    memberBulkUploadTemps.Add(temp);

                }
                await _dbContext.MemberBulkUploadTemp.AddRangeAsync(memberBulkUploadTemps, cancellationToken);

                var bulkUploadSession = new MemberBulkUploadSession
                {
                    CreatedByUserId = request.UploadedByUserId,
                    Description = "Coop member bulk registration upload",
                    DateCreated = DateTime.Now,
                    Size = memberBulkUploadTemps.Count,
                    Status = nameof(MemberProfileStatus.PENDING_APPROVAL),
                    IsActive = true,
                    SessionId = request.SessionId,
                };

                var memberBulkUploadSessionEntity = _dbContext.MemberBulkUploadSessions.Where(s=>s.SessionId==request.SessionId).FirstOrDefault();

                if (memberBulkUploadSessionEntity == null)
                {
                    await _dbContext.MemberBulkUploadSessions.AddAsync(bulkUploadSession, cancellationToken);
                }
                await _dbContext.SaveChangesAsync(cancellationToken);

                memberBulkUploadSessionEntity = await _dbContext.MemberBulkUploadSessions.FirstOrDefaultAsync(x=>x.SessionId==bulkUploadSession.SessionId);

                rsp.Response = new MemberBulkUploadViewModel
                {
                    SessionId = memberBulkUploadSessionEntity?.SessionId,
                };
                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Message = "Data successfully uploaded, checked and migrated valid data for approval.";

                
                var approvalRequest = new CreateApprovalModel
                {
                    Module = "MemberProfileBulkUpload",
                    Payload = System.Text.Json.JsonSerializer.Serialize(request),
                    Comment = "Member Profile Bulk Upload initiated",
                    ApprovalType = ApprovalType.MEMBER_BULK_UPLOAD,
                    Description = $"Bulk Upload - {nameof(MemberBulkUploadSession)}{request.SessionId}",
                    CreatedBy = request.UploadedByUserId,
                    EntityId = memberBulkUploadSessionEntity?.Id,
                    EntityType = typeof(CreateMemberBulkUploadCommand)
                };

                await _approval.CreateApproval(approvalRequest, true);

                return rsp;
            }
            catch (Exception ex)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = ex.Message + ". Data validation could not be completed.Contact the admin.";
               // _logger.LogError(nameof(MemberBulkUploadCommandHandler), nameof(CreateMemberBulkUploadCommand), ex);

                return rsp;
            }


        }

        public async Task<CommandResult<bool>> Handle(NotifyMemberUploadRegistrationCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<bool>();
            foreach (var item in request.RegNotifications)
            {
                var props = new CreateApplicationUserEmailDto()
                {
                    Link = _options.WebBaseUrl,
                    Password = item.Password,
                    Name = item.FirstName
                };
                _ =  _emailService.SendEmailAsync(EmailTemplateType.CREATE_APPLICATION_USER, item.Email, props);
            }
            return rsp;
        }
    }
}
