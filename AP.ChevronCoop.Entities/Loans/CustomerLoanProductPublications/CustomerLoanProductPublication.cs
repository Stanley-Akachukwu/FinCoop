using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Loans.LoanProducts;

namespace AP.ChevronCoop.Entities.Loans.LoanProductPublications.MemberLoanProductPublications;

public class CustomerLoanProductPublication : BaseEntity<string>
{
  public CustomerLoanProductPublication()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }

  public PublicationType PublicationType { get; set; }

  public string ProductId { get; set; }
  public virtual LoanProduct Product { get; set; }

  public string CustomerId { get; set; }
  public virtual Customer Customer { get; set; }

  public override string DisplayCaption { get; }
  public override string DropdownCaption { get; }
  public override string ShortCaption { get; }
}
