

using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;

namespace AP.ChevronCoop.AppCore.Services.AccountAutoCreationServices
{
    public interface IAutoCreateAccountService
    {
        Task<bool> CheckDefaultProductsAsync();
        Task<CommandResult<(bool Completed, string Message)>> CreateSpecialAndSavingsAccountAsync(MemberProfile memberProfile);
        Task<CommandResult<(bool Completed, string Message)>> GetCreateSpecialAndSavingsAccountResultAsync(string applicationUserId);
    }
}
