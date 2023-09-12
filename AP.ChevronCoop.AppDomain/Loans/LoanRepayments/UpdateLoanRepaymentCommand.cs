using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepayments;

public class UpdateLoanRepaymentCommand : UpdateCommand, IRequest<CommandResult<LoanRepaymentViewModel>>
{
    public string Status { get; set; }

    public string RepaymentMode { get; set; }

    public string LoanAccountId { get; set; }

    public string LoanRepaymentScheduleId { get; set; }

    public string PayrollDeductionScheduleItemId { get; set; }

    public string LoanOffsetId { get; set; }

    public string ApprovalId { get; set; }

    public decimal Amount { get; set; }

    public decimal Principal { get; set; }

    public decimal Interest { get; set; }

    public DateTimeOffset? PeriodStartDate { get; set; }

    public DateTimeOffset? RepaymentDate { get; set; }

    public string PaymentAccountId { get; set; }

    public string CustomerBankAccountId { get; set; }

    public string TransactionJournalId { get; set; }
}