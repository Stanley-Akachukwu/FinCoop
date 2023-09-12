using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;

namespace AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;

public class MemberNextOfKin : BaseEntity<string>
{
  public MemberNextOfKin()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }
  
  public string ProfileId { get; set; }

  [ForeignKey(nameof(ProfileId))]
  public virtual MemberProfile MemberProfile { get; set; }
  
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string Relationship { get; set; }
  public string Address { get; set; }
  
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