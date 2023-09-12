using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.AuditTrails;

public class CreateAuditTrailCommand: CreateCommand, IRequest<CommandResult<AuditTrailViewModel>>
{
  public string Module { get; set; }
  public string Payload { get; set; }
  public string Action { get; set; }
  public string IPAddress { get; set; }
  public string UserName { get; set; }
  public DateTime Timestamp { get; set; }
  public string EventType { get; set; }
  public string TableName { get; set; }
  public string PrimaryKey { get; set; }
  public string OldValues { get; set; }
  public string NewValues { get; set; }
  public string AuditJson { get; set; }
}