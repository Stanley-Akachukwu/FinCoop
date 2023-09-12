using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanDisbursementCharges;

public class DeleteLoanDisbursementChargeCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}