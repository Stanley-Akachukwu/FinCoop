using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppCore.Services.MemberprofileServices;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices
{
    public class MemberBulkUploadRetryBackgroundService : IMemberBulkUploadRetryBackgroundService
    {
        private readonly ChevronCoopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IMemberProfileService _memberProfileService;
        private const string className = nameof(MemberProfileService);
        private CancellationTokenSource cts = new CancellationTokenSource();
        public MemberBulkUploadRetryBackgroundService(ChevronCoopDbContext dbContext, IMapper mapper, ILoggerService logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task RetryMemberUploadRegisterationProcessing()
        {
            var memberBulkUploadSessions = await _dbContext.MemberBulkUploadSessions.ToListAsync();
            string methodName = "RetryMemberUploadRegisterationProcessing";

            try
            {
                await _logger.LogInfo(className, methodName, $" Background tasks are being processed ...");

                var memberBulkUploadTemps = new List<MemberBulkUploadTemp>();

                if (memberBulkUploadSessions != null)
                {
                    foreach (var session in memberBulkUploadSessions)
                    {
                        var memberBulkUploads = await _dbContext.MemberBulkUploadTemp.Where(m => m.IsSuccessfullyRegistered == false && m.SessionId == session.Id).ToListAsync();
                        if (memberBulkUploads != null)
                        {
                            foreach (var member in memberBulkUploads)
                            {
                                var approval = await _dbContext.Approvals.FirstOrDefaultAsync(a => a.EntityId == member.ApprovalId && a.IsApprovalCompleted == true);
                                if (approval != null)
                                {
                                    var registerResponse = await _memberProfileService.RegisterMigratedMember(member, cts.Token);
                                    if (registerResponse.StatusCode == (int)HttpStatusCode.OK)
                                    {
                                        member.Status = nameof(MemberProfileStatus.APPROVED);
                                        member.IsSuccessfullyRegistered = true;
                                        member.DateUpdated = DateTime.Now;
                                    }
                                    else
                                    {
                                        member.Status = nameof(MemberProfileStatus.PENDING_APPROVAL);
                                        member.IsSuccessfullyRegistered = false;
                                        member.DateUpdated = DateTime.Now;
                                    }

                                    memberBulkUploadTemps.Add(member);
                                }
                            }

                        }
                    }

                    _dbContext.MemberBulkUploadTemp.UpdateRange(memberBulkUploadTemps);
                    _dbContext.SaveChanges();
                    var sizeCreated = memberBulkUploadSessions.Count;
                    var msg = $" Background tasks completed processing. {sizeCreated} uploaded member registrations processed";
                    await _logger.LogInfo(className, methodName, msg);

                }
            }

            catch (Exception ex)
            {
                await _logger.LogError(className, methodName, ex);
            }
        }
    }
}
