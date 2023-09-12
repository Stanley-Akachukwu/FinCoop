using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;

public class CreateInitializeLoanOffsetCommand : CreateCommand, IRequest<CommandResult<LoanDisbursementViewModel>>
{
    public string LoanAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset? DisbursementDate { get; set; }
    public TransactionType? TransactionType { get; set; }
}