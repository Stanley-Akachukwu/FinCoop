using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class CreateLoanApplicationCommand : IRequest<CommandResult<LoanApplicationViewModel>>
{
    public string LoanProductId { get; set; }
    public string CustomerId { get; set; }
    public decimal Amount { get; set; }
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public DateTime RepaymentCommencementDate { get; set; }
    public bool UseSpecialDeposit { get; set; }
    public string? SpecialDepositId { get; set; }
    public string? DestinationAccountId { get; set; }
    public string? ApplicationUserId { get; set; }
    public List<ApplianceDetails>? Items { get; set; }
    public List<GuarantorDetails> Guarantors { get; set; }
}

public class ApplianceDetails
{
    public LoanApplicationItemType ItemType { get; set; }
    public string Name { get; set; }
    public string BrandName { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public decimal Amount { get; set; }
}

public class GuarantorDetails
{
    public GuarantorType GuarantorType { get; set; }
    public string GuarantorCustomerId { get; set; }
}
