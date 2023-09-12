using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopups;

public class LoanTopupViewModel : BaseViewModel
{
    public string LoanAccountId { get; set; }

    public decimal TopupAmount { get; set; }

    public TopupFundingSourceType DestinationType { get; set; }

    public string SpecialDepositAccountId { get; set; }

    public string CustomerBankAccountId { get; set; }

    public decimal PrincipalBalance { get; set; }

    public decimal InterestBalance { get; set; }

    public DateTimeOffset TopupDate { get; set; }

    public DateTimeOffset CommencementDate { get; set; }
    public List<GuarantorDetails> Guarantors { get; set; }
}