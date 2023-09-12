using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;

namespace AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;

public class BulkUploadApprovalFactory : IApprovalFactory
{
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
   
    public BulkUploadApprovalFactory(ChevronCoopDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
        
    }

    public async Task<CommandResult<bool>> Initiate(Approval request)
    {
        var response = new CommandResult<bool>();

        response.Response = await _mediator.Send(new SendApprovalRequestNotificationCommand()
        {
            ApprovalId = request.Id
        });

        return response;
    }

    public async Task<CommandResult<bool>> Process(Approval request, string? approvedById, string? comment, ApprovalStatus status)
    {
        var response = new CommandResult<bool>();
        try
        {
            var uploadRequest = JsonConvert.DeserializeObject<BulkUploadApprovalModel>(request.Payload);
            if (uploadRequest == null) return response;

            var sessionUpload = await _dbContext.MemberBulkUploadSessions.FirstOrDefaultAsync(x => x.SessionId == uploadRequest.SessionId);
            if (sessionUpload == null) return response;

            sessionUpload.Status = nameof(MemberProfileStatus.APPROVED);
            _dbContext.MemberBulkUploadSessions.Update(sessionUpload);


            var uploadedMembers = await _dbContext.MemberBulkUploadTemp.Where(x => x.SessionId == sessionUpload.SessionId).ToListAsync();
            if (!uploadedMembers.Any()) return response;



            var memberProfiles = new List<MemberProfile>();
            var tupleNotifications = new List<(string Email, string Password, string FirstName)>();
 
            foreach (var member in uploadedMembers)
            {
                MemberType _memberType = (MemberType)Enum.Parse(typeof(MemberType), member.MemberType, true);
                var tmpPassword = StringHelper.GenerateRandomString(10);
                var command = new RegisterMemberCommand
                {
                    TermsAndCondition = true,
                    Email = member.Email,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    IsKycStarted = true,
                    Location = "NA",
                    MembershipId = member.MembershipNumber,
                    Password = tmpPassword,
                    ConfirmPassword = tmpPassword,
                    Role = _memberType,
                };

                var rsp = await _mediator.Send(command);
                if(string.IsNullOrEmpty(rsp?.Response?.UserId)) break;


                var memberProfile = await _dbContext.MemberProfiles.Where(x => x.ApplicationUserId == rsp.Response.UserId).FirstOrDefaultAsync();
                if (memberProfile == null) break;

                memberProfile.Status = MemberProfileStatus.AWAITING_KYC_COMPLETION;
                memberProfiles.Add(memberProfile);
                tupleNotifications.Add((member.Email, tmpPassword, member.FirstName));

            }
            _dbContext.MemberProfiles.UpdateRange(memberProfiles);
            await   _dbContext.SaveChangesAsync();

            var notifyCommand = new NotifyMemberUploadRegistrationCommand
            {
                RegNotifications = tupleNotifications,   
            };

          _=  await _mediator.Send(notifyCommand);
            response.Response = true;
        }
        catch (Exception e)
        {
            response.Response = false;
            response.Message = e.Message;
        }

        return response;
    }
}