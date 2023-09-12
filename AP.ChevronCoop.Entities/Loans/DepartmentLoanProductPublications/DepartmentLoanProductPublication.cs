using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.MasterData.Departments;

namespace AP.ChevronCoop.Entities.Loans.DepartmentLoanProductPublications;

public class DepartmentLoanProductPublication : BaseEntity<string>
{
  public DepartmentLoanProductPublication()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }

  public PublicationType PublicationType { get; set; }

  public string ProductId { get; set; }
  public virtual LoanProduct Product { get; set; }

  public string DepartmentId { get; set; }

  public virtual Department Department { get; set; }


  public override string DisplayCaption { get; }
  public override string DropdownCaption { get; }
  public override string ShortCaption { get; }
}