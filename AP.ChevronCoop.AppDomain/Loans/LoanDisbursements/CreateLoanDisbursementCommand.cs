using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursements;

public class CreateLoanDisbursementCommand : CreateCommand, IRequest<CommandResult<LoanDisbursementViewModel>>
{
    public string LoanAccountId { get; set; }
    public decimal Amount { get; set; }
    public string? DisbursementAccountId { get; set; }
    public DateTimeOffset? DisbursementDate { get; set; }
    public LoanDisbursementMode DisbursementMode { get; set; }
    public string? SpecialDepositAccountId { get; set; }
    public string? CustomerBankAccountId { get; set; }
    public TransactionType? TransactionType { get; set; }
}