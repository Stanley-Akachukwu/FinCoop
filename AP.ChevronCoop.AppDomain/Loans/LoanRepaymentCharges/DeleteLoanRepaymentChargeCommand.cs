using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanRepaymentCharges;

public class DeleteLoanRepaymentChargeCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}