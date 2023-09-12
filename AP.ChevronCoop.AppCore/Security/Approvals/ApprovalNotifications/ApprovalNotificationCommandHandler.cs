using AP.ChevronCoop.AppCore.Services.ApprovalServices;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalNotifications;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalNotifications;

public class ApprovalNotificationCommandHandler :
  IRequestHandler<CreateApprovalNotificationCommand, CommandResult<string>>,
  IRequestHandler<SendApprovalRequestNotificationCommand, bool>,
  IRequestHandler<SendApprovalNotificationCommand, bool>
{
    private readonly IManageApprovalService _approval;
    private readonly CoreAppSettings _options;
    private readonly ChevronCoopDbContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly ILogger<ApprovalNotificationCommandHandler> _logger;
    private readonly IMapper _mapper;

    public ApprovalNotificationCommandHandler(ChevronCoopDbContext appDbContext, IEmailService emailService,
      ILogger<ApprovalNotificationCommandHandler> logger, IMapper mapper, IManageApprovalService approval, IOptions<CoreAppSettings> options)
    {
        _dbContext = appDbContext;
        _emailService = emailService;
        _logger = logger;
        _mapper = mapper;
        _approval = approval;
        _options = options.Value;
    }

    public async Task<CommandResult<string>> Handle(CreateApprovalNotificationCommand request, CancellationToken cancellationToken)
    {
        var rsp = new CommandResult<string>();
        ApprovalNotification approvalNotification = _mapper.Map<ApprovalNotification>(request);
        approvalNotification.Escalation = System.Text.Json.JsonSerializer.Serialize(new ApprovalExcalation
        {
            ExcalationUserId = request.EscalateToUserIds,
            ExclationTriggerCount = request.MaxReminderCount
        });
        approvalNotification.Reminder = System.Text.Json.JsonSerializer.Serialize(new ApprovalReminder
        {
            ReminderUserIds = request.EscalateToUserIds,
            ReminderCount = request.MaxReminderCount,
            ReminderTriggerTime = request.ReminderTriggerTime
        });

        _dbContext.ApprovalNotifications.Add(approvalNotification);
        await _dbContext.SaveChangesAsync();
        rsp.StatusCode = (int)HttpStatusCode.OK;
        rsp.Message = $"Notification created and tied to approval workflow Id-{request.ApprovalWorkflowId}";
        rsp.Response = $"Notification ID -{approvalNotification.Id}";
        return rsp;
    }

    public async Task<bool> Handle(SendApprovalRequestNotificationCommand request, CancellationToken cancellationToken)
    {
        try
        {



            var approval = await _dbContext.Approvals
              .FirstOrDefaultAsync(x => x.Id == request.ApprovalId, cancellationToken: cancellationToken);

            var approvalGroup = await _dbContext.ApprovalGroupWorkflows
              .Where(x => x.ApprovalWorkflowId == approval!.ApprovalWorkflowId)
              .OrderBy(o => o.Sequence)
              .Skip(request.Sequence)
              .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var members = await _dbContext.ApprovalGroupMembers
              .Where(x => x.ApprovalGroupId == approvalGroup!.Id)
              .ToListAsync(cancellationToken: cancellationToken);

            foreach (var member in members)
            {
                var profile = await _dbContext.MemberProfiles.Where(x => x.ApplicationUserId == member.ApplicationUserId)
                  .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (profile == null) continue;
                var props = new GeneralEmailDto
                {
                    Link = $"{_options.WebBaseUrl}",
                    Name = $"{profile.FirstName} {profile.LastName}"
                };

                if (!string.IsNullOrWhiteSpace(profile?.PrimaryEmail))
                    await _emailService.SendEmailAsync(EmailTemplateType.APPROVAL_NOTIFICATION, profile.PrimaryEmail, props);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> Handle(SendApprovalNotificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var approval = await _dbContext.Approvals
                .FirstOrDefaultAsync(x => x.Id == request.ApprovalId, cancellationToken: cancellationToken);

            switch (approval.ApprovalType)
            {
                case ApprovalType.LOAN_PRODUCT_APPLICATION:
                    {
                        var loan = await _dbContext.LoanApplications
                            .Include(p => p.LoanProduct).ThenInclude(c => c.DefaultCurrency)
                            .FirstOrDefaultAsync(x => x.Id == approval.EntityId, cancellationToken: cancellationToken);

                        var profile = await _dbContext.Customers.Where(x => x.ApplicationUserId == loan!.CustomerId)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        if (profile == null) return false;

                        var props = new GeneralEmailDto
                        {
                            Link = $"{_options.WebBaseUrl}",
                            Name = $"{profile.FirstName} {profile.LastName}",
                            Description = request.IsApproved ?
                                $"Your pending <b>{loan.LoanProduct.Name}</b> application of {loan.LoanProduct.DefaultCurrency.Symbol}{loan.Principal} has been approved."
                                : $"Your pending <b>{loan.LoanProduct.Name}</b> application of {loan.LoanProduct.DefaultCurrency.Symbol}{loan.Principal} was rejected" +
                                  $"because <i>{approval.Comment}</i>"
                        };

                        _ = _emailService.SendEmailAsync(EmailTemplateType.APPROVAL_REJECTION_NOTIFICATION, profile.PrimaryEmail, props);
                        break;
                    }

                case ApprovalType.LOAN_TOPUP_APPLICATION:
                    {
                        var loan = await _dbContext.LoanTopups
                            .Include(p => p.LoanAccount)
                            .FirstOrDefaultAsync(x => x.Id == approval.EntityId, cancellationToken: cancellationToken);

                        var profile = await _dbContext.Customers.Where(x => x.ApplicationUserId == loan!.LoanAccount.CustomerId)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        if (profile == null) return false;

                        var props = new GeneralEmailDto
                        {
                            Link = $"{_options.WebBaseUrl}",
                            Name = $"{profile.FirstName} {profile.LastName}",
                            Description = request.IsApproved ? $"Your pending loan top-up on <b>{loan.LoanAccount.AccountNo}</b> has been approved."
                                : $"Your pending loan top-up on <b>{loan.LoanAccount.AccountNo}</b> was rejected" +
                                          $"because <i>{approval.Comment}</i>"
                        };

                        _ = _emailService.SendEmailAsync(EmailTemplateType.APPROVAL_REJECTION_NOTIFICATION, profile.PrimaryEmail, props);
                        break;
                    }

                case ApprovalType.LOAN_OFFSET_APPLICATION:
                    {
                        var loan = await _dbContext.LoanOffsets
                            .Include(p => p.LoanAccount)
                            .FirstOrDefaultAsync(x => x.Id == approval.EntityId, cancellationToken: cancellationToken);

                        var profile = await _dbContext.Customers.Where(x => x.ApplicationUserId == loan!.LoanAccount.CustomerId)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                        if (profile == null) return false;

                        var props = new GeneralEmailDto
                        {
                            Link = $"{_options.WebBaseUrl}",
                            Name = $"{profile.FirstName} {profile.LastName}",
                            Description = request.IsApproved ? $"Your pending loan offset on <b>{loan.LoanAccount.AccountNo}</b> has been approved."
                                : $"Your pending loan offset on <b>{loan.LoanAccount.AccountNo}</b> was rejected" +
                                          $"because <i>{approval.Comment}</i>"
                        };

                        _ = _emailService.SendEmailAsync(EmailTemplateType.APPROVAL_REJECTION_NOTIFICATION, profile.PrimaryEmail, props);
                        break;
                    }
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}