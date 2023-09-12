using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;

public class QueryMemberBulkUploadCommand : IRequest<CommandResult<IQueryable<MemberBulkUploadTemp>>>
{
}