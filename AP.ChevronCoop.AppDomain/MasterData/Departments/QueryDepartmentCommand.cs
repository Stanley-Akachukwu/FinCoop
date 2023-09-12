using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData;
using AP.ChevronCoop.Entities.MasterData.Departments;
using MediatR;

namespace AP.ChevronCoop.AppDomain.MasterData.Departments
{
    public class QueryDepartmentCommand : IRequest<CommandResult<IQueryable<Department>>>
    {

    }







}
