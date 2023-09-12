using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;

public class DeleteLoanProductChargeCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}