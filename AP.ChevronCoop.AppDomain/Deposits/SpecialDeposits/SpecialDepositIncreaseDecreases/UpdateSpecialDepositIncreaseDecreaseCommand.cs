using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
public partial class UpdateSpecialDepositIncreaseDecreaseCommand : UpdateCommand, IRequest<CommandResult<SpecialDepositIncreaseDecreaseViewModel>>
{

    public string SpecialDepositAccountId { get; set; }
    public decimal Amount { get; set; }
    public ContributionChangeRequest ContributionChangeRequest { get; set; }

}


