using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;

namespace AP.ChevronCoop.Entities.CoopCustomers.CustomerBeneficiaries;

public class CustomerBeneficiary : BaseEntity<string>
{
  public CustomerBeneficiary()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }

  public string CustomerId { get; set; }

  // [ForeignKey(nameof(CustomerId))]
  public virtual Customer Customer { get; set; }

  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string Address { get; set; }

  public override string DisplayCaption => "";

  public override string DropdownCaption => "";

  public override string ShortCaption => "";
}