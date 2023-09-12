using AP.ChevronCoop.Commons;

namespace AP.ChevronCoop.Entities.MasterData.Banks;

public class Bank : BaseEntity<string>
{
  public Bank()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }


  public string Code { get; set; }
  public string SortCode { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public string ContactName { get; set; }
  public string ContactDetails { get; set; }

  public override string DisplayCaption => "";

  public override string DropdownCaption => "";

  public override string ShortCaption => "";
}