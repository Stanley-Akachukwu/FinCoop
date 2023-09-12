using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public record GetLoanProductCommand(string Id) : IRequest<CommandResult<GetLoanProductViewModel>>;