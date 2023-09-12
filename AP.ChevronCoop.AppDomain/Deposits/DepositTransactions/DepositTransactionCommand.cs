using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.DepositTransactions;

public class DepositTransactionCommand : IRequest<CommandResult<DepositTransactionViewModel>>
{
  public DepositTransactionCommand()
  {
    TransactionDate = DateTime.Now;
    IsApproved = false;
  }

  public string DepositAccountId { get; set; }
  public string? TransactionJournalId { get; set; }

  public TransactionType TransactionType { get; set; }
  public TransactionAction TransactionAction { get; set; }
  public string EntityId { get; set; }
  public Type EntityType { get; set; }

  public DateTime TransactionDate { get; set; }

  public bool IsApproved { get; set; }
  public string? ApprovalId { get; set; }
  public DateTime? ApprovedOn { get; set; }
}