using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Employees;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Employees
{
    public class QueryEmployeeCommand : IRequest<CommandResult<IQueryable<Employee>>>
    {

    }







}
