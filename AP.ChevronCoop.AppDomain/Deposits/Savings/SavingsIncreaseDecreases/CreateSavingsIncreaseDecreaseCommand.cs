using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
public partial class CreateSavingsIncreaseDecreaseCommand : CreateCommand, IRequest<CommandResult<SavingsIncreaseDecreaseViewModel>>
{

    public string CustomerId { get; set; }
    public string SavingsAccountId { get; set; }

    public decimal Amount { get; set; }

    public ContributionChangeRequest  ContributionChangeRequest { get; set; }

}


