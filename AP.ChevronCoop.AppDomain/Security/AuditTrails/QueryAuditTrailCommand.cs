using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.AuditTrails;

public class QueryAuditTrailCommand: IRequest<CommandResult<IQueryable<AuditTrail>>>
{
  
}