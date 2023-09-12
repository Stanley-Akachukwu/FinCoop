using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopups;

public class UpdateLoanTopupCommand : UpdateCommand, IRequest<CommandResult<LoanTopupViewModel>>
{
  public string LoanAccountId { get; set; }

  public decimal TopupAmount { get; set; }

  public string DestinationType { get; set; }

  public string SpecialDepositAccountId { get; set; }

  public string CustomerBankAccountId { get; set; }

  public decimal OldPrincipalBalance { get; set; }

  public decimal NewPrincipalBalance { get; set; }

  public decimal OldInterestBalance { get; set; }

  public decimal NewInterestBalance { get; set; }

  public decimal TotalTopupCharges { get; set; }

  public DateTimeOffset TopupDate { get; set; }

  public DateTimeOffset CommencementDate { get; set; }

  public string TransactionJournalId { get; set; }

  public string ApprovalId { get; set; }
}