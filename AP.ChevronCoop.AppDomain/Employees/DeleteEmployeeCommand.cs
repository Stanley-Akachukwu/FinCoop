using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Employees
{
    public partial class DeleteEmployeeCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }







}
