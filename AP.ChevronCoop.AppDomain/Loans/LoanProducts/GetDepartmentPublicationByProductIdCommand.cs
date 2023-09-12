using AP.ChevronCoop.AppDomain.MasterData.Departments;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public record GetDepartmentPublicationByProductIdCommand
  (string ProductId) : IRequest<CommandResult<List<DepartmentViewModel>>>;