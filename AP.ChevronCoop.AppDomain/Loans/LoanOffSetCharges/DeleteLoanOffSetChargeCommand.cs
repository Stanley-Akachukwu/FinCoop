using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffSetCharges;

public class DeleteLoanOffSetChargeCommand : DeleteCommand, IRequest<CommandResult<string>>
{
}