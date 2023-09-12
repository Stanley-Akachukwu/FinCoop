using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanAccounts;

public record GetLoanAccountCommand(string Id) : IRequest<CommandResult<GetLoanAccountViewModel>>;