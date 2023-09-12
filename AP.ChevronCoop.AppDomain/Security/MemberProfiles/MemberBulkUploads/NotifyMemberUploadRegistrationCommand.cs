using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;

public class NotifyMemberUploadRegistrationCommand : IRequest<CommandResult<bool>>
{
    public List<(string Email, string Password, string FirstName)> RegNotifications { get; set; }
}


