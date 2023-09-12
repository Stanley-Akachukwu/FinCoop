using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.Charges;
using MediatR;

namespace AP.ChevronCoop.AppDomain.MasterData.Charges
{
    public class QueryChargeCommand : IRequest<CommandResult<IQueryable<Charge>>>
    {

    }







}
