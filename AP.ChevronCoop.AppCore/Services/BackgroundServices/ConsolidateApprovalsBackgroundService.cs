using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.Entities;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices;

public class ConsolidateApprovalsBackgroundService : IConsolidateApprovalsBackgroundService
{
  private readonly ChevronCoopDbContext _dbContext;
  private readonly ILoggerService _logger;
  private readonly IMapper _mapper;
  private CancellationTokenSource cts = new();

  public ConsolidateApprovalsBackgroundService(ChevronCoopDbContext dbContext, IMapper mapper, ILoggerService logger)
  {
    _dbContext = dbContext;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task ExecuteApprovalsConsolidation()
  {
    // try
    // {
    //     #region 
    //     var admin = _dbContext.ApplicationRoles.FirstOrDefault(r => r.Name.ToLower() == "admin");
    //     if (admin != null)
    //     {
    //
    //         var adminMembers = (from m in _dbContext.MemberProfiles join r in _dbContext.ApplicationUserRoles on m.ApplicationUserId equals r.UserId where r.RoleId == admin.Id where m.Status == MemberProfileStatus.PENDING_APPROVAL select m).ToList();
    //
    //         if (adminMembers.Count > 0)
    //         {
    //             var adminMembersRequestConsolidation = await _dbContext.ApprovalConsolidations.Where(a => a.RequestName == "AdminMembers").FirstOrDefaultAsync();
    //             if (adminMembersRequestConsolidation == null)
    //             {
    //                 var consolidation = new ApprovalConsolidation();
    //                 consolidation.Size = adminMembers.Count;
    //                 consolidation.RequestName = "AdminMembers";
    //                 consolidation.Description = "List of adminMembers Awaiting Approval.";
    //                 consolidation.DateCreated = DateTime.Now;
    //                 consolidation.DateUpdated = DateTime.Now;
    //                 consolidation.AprrovalProcessingPage = "/security/users";
    //                 _dbContext.ApprovalConsolidations.Add(consolidation);
    //             }
    //             else
    //             {
    //                 adminMembersRequestConsolidation.Size = adminMembers.Count;
    //                 adminMembersRequestConsolidation.DateUpdated = DateTime.Now;
    //                 adminMembersRequestConsolidation.UpdatedByUserId = adminMembers[0].CreatedByUserId;
    //                 _dbContext.ApprovalConsolidations.Update(adminMembersRequestConsolidation);
    //             }
    //         }
    //
    //     }
    //
    //
    //
    //     var kyclist = await _dbContext.MemberProfiles.Where(a => a.Status == MemberProfileStatus.AWAITING_KYC_APPROVAL).ToListAsync(cancellationToken: cts.Token);
    //     if (kyclist.Count > 0)
    //     {
    //         var kyclistRequestConsolidation = await _dbContext.ApprovalConsolidations.Where(a => a.RequestName == "KYCCompletion").FirstOrDefaultAsync();
    //         if (kyclistRequestConsolidation == null)
    //         {
    //             var consolidation = new ApprovalConsolidation();
    //             consolidation.Size = kyclist.Count;
    //             consolidation.RequestName = "KYCCompletion";
    //             consolidation.Description = "List of Completed KYC Awaiting Approval.";
    //             consolidation.DateCreated = DateTime.Now;
    //             consolidation.DateUpdated = DateTime.Now;
    //             consolidation.AprrovalProcessingPage = "/security/kyc-approvals";
    //             _dbContext.ApprovalConsolidations.Add(consolidation);
    //         }
    //         else
    //         {
    //             kyclistRequestConsolidation.Size = kyclist.Count;
    //             kyclistRequestConsolidation.DateUpdated = DateTime.Now;
    //             kyclistRequestConsolidation.UpdatedByUserId = kyclist[0].CreatedByUserId;
    //             _dbContext.ApprovalConsolidations.Update(kyclistRequestConsolidation);
    //         }
    //     }
    //
    //     var memberBulkUploadSessions = await _dbContext.MemberBulkUploadSessions.Where(a => a.Status == nameof(MemberProfileStatus.PENDING_APPROVAL)).ToListAsync();
    //     if (memberBulkUploadSessions.Count > 0)
    //     {
    //         var bulkUploadRequestConsolidation = await _dbContext.ApprovalConsolidations.Where(a => a.RequestName == "MemberBulkUpload").FirstOrDefaultAsync();
    //         if (bulkUploadRequestConsolidation == null)
    //         {
    //             var consolidation = new ApprovalConsolidation();
    //             consolidation.Size = memberBulkUploadSessions.Count;
    //             consolidation.RequestName = "MemberBulkUpload";
    //             consolidation.Description = "List of Member Bulk Upload Awaiting Approval.";
    //             consolidation.DateCreated = DateTime.Now;
    //             consolidation.DateUpdated = DateTime.Now;
    //             consolidation.AprrovalProcessingPage = "/Security/MigrationApproval";
    //             _dbContext.ApprovalConsolidations.Add(consolidation);
    //         }
    //         else
    //         {
    //             bulkUploadRequestConsolidation.Size = memberBulkUploadSessions.Count;
    //             bulkUploadRequestConsolidation.DateUpdated = DateTime.Now;
    //             bulkUploadRequestConsolidation.UpdatedByUserId = memberBulkUploadSessions[0].CreatedByUserId;
    //             _dbContext.ApprovalConsolidations.Update(bulkUploadRequestConsolidation);
    //         }
    //     }
    //
    //     var retireeSwitches = await _dbContext.RetireeSwitches.Where(a => a.Status == nameof(MemberProfileStatus.PENDING_APPROVAL)).ToListAsync();
    //     if (retireeSwitches.Count > 0)
    //     {
    //         var retireeSwitchRequestConsolidation = await _dbContext.ApprovalConsolidations.Where(a => a.RequestName == "RetireeSwitch").FirstOrDefaultAsync();
    //         if (retireeSwitchRequestConsolidation == null)
    //         {
    //             var consolidation = new ApprovalConsolidation();
    //             consolidation.Size = retireeSwitches.Count;
    //             consolidation.RequestName = "RetireeSwitch";
    //             consolidation.Description = "List of Retiree Switch Request Awaiting Approval.";
    //             consolidation.DateCreated = DateTime.Now;
    //             consolidation.DateUpdated = DateTime.Now;
    //             consolidation.AprrovalProcessingPage = "/approval/retirees";
    //
    //             _dbContext.ApprovalConsolidations.Add(consolidation);
    //         }
    //         else
    //         {
    //             retireeSwitchRequestConsolidation.Size = retireeSwitches.Count;
    //             retireeSwitchRequestConsolidation.DateUpdated = DateTime.Now;
    //             retireeSwitchRequestConsolidation.UpdatedByUserId = retireeSwitches[0].CreatedByUserId;
    //             _dbContext.ApprovalConsolidations.Update(retireeSwitchRequestConsolidation);
    //         }
    //     }
    //
    //
    //     var memberProfiles = await _dbContext.MemberProfiles.Where(a => a.Status == (MemberProfileStatus.PENDING_APPROVAL)).ToListAsync();
    //     if (memberProfiles.Count > 0)
    //     {
    //         var memberProfilesRequestConsolidation = await _dbContext.ApprovalConsolidations.Where(a => a.RequestName == "MemberEnrollment").FirstOrDefaultAsync();
    //         if (memberProfilesRequestConsolidation == null)
    //         {
    //             var consolidation = new ApprovalConsolidation();
    //             consolidation.Size = memberProfiles.Count;
    //             consolidation.RequestName = "MemberEnrollment";
    //             consolidation.Description = "List of MemberProfiles Request Awaiting Approval.";
    //             consolidation.DateCreated = DateTime.Now;
    //             consolidation.DateUpdated = DateTime.Now;
    //             consolidation.AprrovalProcessingPage = "/security/enrollments";
    //             _dbContext.ApprovalConsolidations.Add(consolidation);
    //         }
    //         else
    //         {
    //             memberProfilesRequestConsolidation.Size = memberProfiles.Count;
    //             memberProfilesRequestConsolidation.DateUpdated = DateTime.Now;
    //             memberProfilesRequestConsolidation.UpdatedByUserId = memberProfiles[0].CreatedByUserId;
    //             _dbContext.ApprovalConsolidations.Update(memberProfilesRequestConsolidation);
    //         }
    //     }
    //
    //
    //     var depositProducts = await _dbContext.DepositProducts.Where(a => a.Status == nameof(ProductStatus.PENDING_APPROVAL)).ToListAsync();
    //     if (depositProducts.Count > 0)
    //     {
    //         var depositSetupRequestConsolidation = await _dbContext.ApprovalConsolidations.Where(a => a.RequestName == "DepositProductSetup").FirstOrDefaultAsync();
    //         if (depositSetupRequestConsolidation == null)
    //         {
    //             var consolidation = new ApprovalConsolidation();
    //             consolidation.Size = 1;
    //             consolidation.RequestName = "DepositProductSetup";
    //             consolidation.Description = "List of Deposit Product Setup Request Awaiting Approval.";
    //             consolidation.DateCreated = DateTime.Now;
    //             consolidation.DateUpdated = DateTime.Now;
    //             consolidation.AprrovalProcessingPage = "/ProductSetup/Manage/all";
    //             _dbContext.ApprovalConsolidations.Add(consolidation);
    //         }
    //         else
    //         {
    //             depositSetupRequestConsolidation.Size = depositProducts.Count;
    //             depositSetupRequestConsolidation.DateUpdated = DateTime.Now;
    //             depositSetupRequestConsolidation.UpdatedByUserId = depositProducts[0].CreatedByUserId;
    //             _dbContext.ApprovalConsolidations.Update(depositSetupRequestConsolidation);
    //         }
    //     }
    //
    //
    //     var loanProducts = await _dbContext.LoanProducts.Where(a => a.Status == ProductStatus.PENDING).ToListAsync();
    //     if (loanProducts.Count > 0)
    //     {
    //         var loanSetupRequestConsolidation = await _dbContext.ApprovalConsolidations.Where(a => a.RequestName == "LoanProductSetup").FirstOrDefaultAsync();
    //         if (loanSetupRequestConsolidation == null)
    //         {
    //             var consolidation = new ApprovalConsolidation();
    //             consolidation.Size = 1;
    //             consolidation.RequestName = "LoanProductSetup";
    //             consolidation.Description = "List of Loan Product Setup Request Awaiting Approval.";
    //             consolidation.DateCreated = DateTime.Now;
    //             consolidation.DateUpdated = DateTime.Now;
    //             consolidation.AprrovalProcessingPage = "/loans/products";
    //             _dbContext.ApprovalConsolidations.Add(consolidation);
    //         }
    //         else
    //         {
    //             loanSetupRequestConsolidation.Size = loanProducts.Count;
    //             loanSetupRequestConsolidation.DateUpdated = DateTime.Now;
    //             loanSetupRequestConsolidation.UpdatedByUserId = loanProducts[0].CreatedByUserId;
    //             _dbContext.ApprovalConsolidations.Update(loanSetupRequestConsolidation);
    //         }
    //     }
    //
    //     #endregion
    //
    //
    //     await _dbContext.SaveChangesAsync();
    // }
    // catch (Exception ex)
    // {
    //     await _logger.LogError(nameof(ConsolidateApprovalsBackgroundService), nameof(ExecuteApprovalsConsolidation), ex);
    // }
  }
}