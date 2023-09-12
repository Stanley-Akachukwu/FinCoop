using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public record GetCustomerPublicationByProductIdCommand
  (string ProductId) : IRequest<CommandResult<List<CustomerViewModel>>>;