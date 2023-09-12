using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplications;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationItems;

public class LoanApplicationItem : BaseEntity<string>
{

    public LoanApplicationItem()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string LoanApplicationId { get; set; }
    public virtual LoanApplication LoanApplication { get; set; }
    public LoanApplicationItemType ItemType { get; set; }
    public string Name { get; set; }
    public string BrandName { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Amount { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}