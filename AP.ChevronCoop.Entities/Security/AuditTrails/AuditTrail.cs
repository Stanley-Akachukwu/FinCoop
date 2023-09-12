using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;

namespace AP.ChevronCoop.Entities.Security.AuditTrails;

public class AuditTrail : BaseEntity<string>
{
  public AuditTrail()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }

  public string ApplicationUserId { get; set; }

  [ForeignKey(nameof(ApplicationUserId))]
  public virtual ApplicationUser ApplicationUser { get; set; }
  
  public string EventGlobalCodeId { get; set; }

  [ForeignKey(nameof(EventGlobalCodeId))]
  public virtual GlobalCode Event { get; set; }


  public string UserName { get; set; }
  public DateTime Timestamp { get; set; }
  public string EventType { get; set; }
  public string TableName { get; set; }
  public string PrimaryKey { get; set; }
  public string OldValues { get; set; }
  public string NewValues { get; set; }
  public string AuditJson { get; set; }
  
  public string Module { get; set; }
  public string Payload { get; set; }
  public string Action { get; set; }
  public string IPAddress { get; set; }
  
  public override string DisplayCaption
  {
    get
    {
      return "";
    }
  }

  public override string DropdownCaption
  {
    get
    {
      return "";
    }
  }

  public override string ShortCaption
  {
    get
    {
      return "";
    }
  }
}