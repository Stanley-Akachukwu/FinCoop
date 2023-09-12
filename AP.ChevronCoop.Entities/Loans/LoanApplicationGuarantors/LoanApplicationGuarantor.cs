using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Loans.LoanApplications;

namespace AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;

public class LoanApplicationGuarantor : BaseEntity<string>
{

    public LoanApplicationGuarantor()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string LoanApplicationId { get; set; }
    public virtual LoanApplication LoanApplication { get; set; }

    public GuarantorType GuarantorType { get; set; }
    public string GuarantorId { get; set; }
    public virtual Customer Guarantor { get; set; }

    public ApprovalStatus Status { get; set; }
    public DateTime? ApprovedOn { get; set; }
    public string Comment { get; set; }
    public GuarantorApprovalType GuarantorApprovalType { get; set; }

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}