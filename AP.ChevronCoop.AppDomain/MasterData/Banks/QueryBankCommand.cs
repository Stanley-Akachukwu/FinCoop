using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData;
using AP.ChevronCoop.Entities.MasterData.Banks;
using MediatR;

namespace AP.ChevronCoop.AppDomain.MasterData.Banks
{
    public class QueryBankCommand : IRequest<CommandResult<IQueryable<Bank>>>
    {

    }







}
