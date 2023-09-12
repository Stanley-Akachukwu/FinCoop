using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts
{

    public partial class DeleteLedgerAccountCommand : DeleteCommand, IRequest<CommandResult<string>>
    {

    }


}
