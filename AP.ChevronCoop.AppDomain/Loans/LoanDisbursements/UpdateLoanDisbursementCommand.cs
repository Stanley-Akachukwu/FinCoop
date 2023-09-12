using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;

public class UpdateLoanDisbursementCommand : UpdateCommand, IRequest<CommandResult<LoanDisbursementViewModel>>
{
  public string Status { get; set; }

  public string LoanAccountId { get; set; }

  public decimal Amount { get; set; }


  public DateTimeOffset? DisbursementDate { get; set; }


  public DateTimeOffset? ApprovalDate { get; set; }

  public string ApprovedByUserId { get; set; }

  public string DisbursedByUserId { get; set; }

  public string DisbursementStatus { get; set; }

  public string DisbursementAccountId { get; set; }

  public string CustomerBankAccountId { get; set; }

  public string TransactionJournalId { get; set; }

  public string ApprovalId { get; set; }

  public string LoanAccountId1 { get; set; }
}