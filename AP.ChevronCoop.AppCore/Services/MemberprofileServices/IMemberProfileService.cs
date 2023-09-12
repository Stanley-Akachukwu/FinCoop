using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;

namespace AP.ChevronCoop.AppCore.Services.MemberprofileServices
{
    public interface IMemberProfileService
    {
        Task<CommandResult<string>> RegisterMigratedMember(MemberBulkUploadTemp request, CancellationToken cancellationToken);
    }
}
