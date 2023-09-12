using AP.ChevronCoop.AppCore.Services.BackgroundServices.Interfaces;
using AP.ChevronCoop.AppCore.Services.LogServices;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalNotifications;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AP.ChevronCoop.AppCore.Services.BackgroundServices;

public class ApprovalNotificationBackgoundService : IApprovalNotificationBackgoundService
{
  private readonly ChevronCoopDbContext _dbContext;
  private readonly ILoggerService _loggerService;
  private readonly IEmailService _emailService;
  private readonly CancellationTokenSource cts = new();

  /// <summary>
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="loggerService"></param>
  /// <param name="emailService"></param>
  public ApprovalNotificationBackgoundService(ChevronCoopDbContext dbContext, ILoggerService loggerService,
    IEmailService emailService)
  {
    _dbContext = dbContext;
    _loggerService = loggerService;
    _emailService = emailService;
  }

  public async Task ExecutNotificationProcess()
  {
    try
    {
      await ExecuteReminderAndExcalationAsync();
    }
    catch (Exception ex)
    {
      await _loggerService.LogInfo(typeof(ApprovalNotificationBackgoundService).Name, "ExecutNotificationProcess",
        $"{ex.Message} ");
    }
  }


  #region private methods

  private async Task<bool> SendNotificationAsync(string email)
  {
    var emailBody = @"
                    <html>
                        <body>
                            <p>Kindly be reminded to login on Chevron Coop portal to approve request. </p>
                        </body>
                    </html>
                ";

    var message = new Message(email, "CEMCS Approval Notification", emailBody);
    await _emailService.SendEmailAsync(message);
    return await Task.FromResult(true);
  }

  private async Task<IList<NotificationResponse>> GetUsersSelectedForReminderNotificationAsync()
  {
    List<ApprovalType> approvalTypes = new()
    {
      ApprovalType.DEPOSIT_PRODUCT,
      ApprovalType.LOAN_PRODUCT,
      ApprovalType.KYC_COMPLETION
    };

        var notifications = _dbContext.Approvals
            .Join(_dbContext.ApprovalNotifications, approval => approval.ApprovalWorkflowId,
                appnotification => appnotification.ApprovalWorkflowId,
                (approval, appnotification) => new { approval, appnotification })
            .Where(@t =>
                @t.approval.IsActive && approvalTypes.Contains(@t.approval.ApprovalType) &&
                @t.approval.Status == ApprovalStatus.INITIATED && @t.appnotification.IsActive)
            .Select(@t => new NotificationResponse
            {
                ApprovalNotificationId = @t.appnotification.ApprovalWorkflowId,
                Payload = @t.approval.Payload,
                ApprovalWorkflowId = @t.approval.ApprovalWorkflowId,
                Reminder = @t.appnotification.Reminder,
                Excalation = @t.appnotification.Escalation
            });

        return await notifications.ToListAsync(cts.Token);

  }


  // Reminders
  // Escalations 
  private async Task ExecuteReminderAndExcalationAsync()
  {
    var approvalNotifications = await GetUsersSelectedForReminderNotificationAsync();

        foreach (var userNotification in approvalNotifications)
        {
            // Deserilaize Reminder and Escalation json payload
            var approvalReminder = JsonConvert.DeserializeObject<ApprovalReminder>(userNotification.Reminder ?? "");
            var approvalExcalation = JsonConvert.DeserializeObject<ApprovalExcalation>(userNotification.Excalation ?? "");

            if (approvalReminder != null)
            {
                // Check if the user meet the Reminder condition
                if (approvalReminder.ReminderCount < approvalExcalation?.ExclationTriggerCount)
                {
                    var approvalNotifyReminder = await _dbContext.ApprovalNotifications.FirstOrDefaultAsync(c => c.Id == userNotification.ApprovalNotificationId);

                    if (approvalNotifyReminder != null)
                    {
                        approvalReminder.ReminderCount++;

                        approvalReminder.ReminderUserIds?.ForEach(s => GetCustomerAndSendNotification(s));

                        approvalNotifyReminder.Reminder = JsonConvert.SerializeObject(approvalReminder);

                        _dbContext.ApprovalNotifications.Update(approvalNotifyReminder);
                        await _dbContext.SaveChangesAsync(cts.Token);
                    }
                    else if (approvalReminder.ReminderCount > approvalExcalation?.ExclationTriggerCount) // Escalations
                    {
                        var approvalNotifyExcalation = await _dbContext.ApprovalNotifications.FirstOrDefaultAsync(c => c.Id == userNotification.ApprovalNotificationId);

                        if (approvalNotifyExcalation != null)
                        {
                            approvalExcalation.ExcalationUserId?.ForEach(async escalationUserId =>
                            {
                                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == escalationUserId, cts.Token);

                                if (customer != null)
                                {
                                    var emailResponse = await SendNotificationAsync(customer.CustomerNo);
                                    await _loggerService.LogInfo(typeof(IApprovalNotificationBackgoundService).Name,
                                        "ExecuteReminderAndExcalationAsync", $"Email Response :{emailResponse}");
                                }
                            });

                            approvalNotifyExcalation.IsActive = false;

                            _dbContext.ApprovalNotifications.Update(approvalNotifyExcalation);
                            await _dbContext.SaveChangesAsync(cts.Token);
                        }
                    }

                }

            }
        }
    }

  private async void GetCustomerAndSendNotification(string reminderUserId)
  {
    var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == reminderUserId, cts.Token);

    if (customer == null) return;
    var emailResponse = await SendNotificationAsync(customer.CustomerNo);
    await _loggerService.LogInfo(typeof(IApprovalNotificationBackgoundService).Name,
      "ExecuteReminderAndExcalationAsync", $"Email Response :{emailResponse}");
  }

  #endregion
}