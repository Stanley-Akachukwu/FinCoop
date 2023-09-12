using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;

namespace AP.ChevronCoop.Entities.Loans.LoanApplications;

public class LoanApplication : BaseEntity<string>
{

    public LoanApplication()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string ApplicationNo { get; set; }
    public string? AccountNo { get; set; } // Just a read only property
    public string LoanProductId { get; set; }
    public virtual LoanProduct LoanProduct { get; set; }
    public string? ApprovalId { get; set; }
    public Approval? Approval { get; set; }
    public string CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public decimal Principal { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public DateTimeOffset RepaymentCommencementDate { get; set; }
    public bool UseSpecialDeposit { get; set; }
    public string? SpecialDepositId { get; set; }
    public virtual SpecialDepositAccount? SpecialDeposit { get; set; }
    public string? CustomerDisbursementAccountId { get; set; }
    public virtual CustomerBankAccount? CustomerDisbursementAccount { get; set; }


    public string? QualificationTargetProductId { get; set; }
    public LoanApplicationStatus Status { get; set; }
    public DepositProductType QualificationTargetProductType { get; set; }


    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }



}