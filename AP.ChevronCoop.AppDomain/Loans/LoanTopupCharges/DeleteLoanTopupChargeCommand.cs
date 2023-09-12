using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;

public class DeleteLoanTopupChargeCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}