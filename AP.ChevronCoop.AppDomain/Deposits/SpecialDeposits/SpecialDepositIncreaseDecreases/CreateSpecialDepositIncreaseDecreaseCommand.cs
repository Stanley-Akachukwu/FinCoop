using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
public partial class CreateSpecialDepositIncreaseDecreaseCommand : CreateCommand, IRequest<CommandResult<SpecialDepositIncreaseDecreaseViewModel>>
{

    public string CustomerId { get; set; }
    public string SpecialDepositAccountId { get; set; }

    public decimal Amount { get; set; }

    public ContributionChangeRequest ContributionChangeRequest { get; set; }

}


