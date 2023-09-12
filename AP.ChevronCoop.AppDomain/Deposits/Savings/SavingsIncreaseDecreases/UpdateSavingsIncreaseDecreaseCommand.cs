using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
public partial class UpdateSavingsIncreaseDecreaseCommand : UpdateCommand, IRequest<CommandResult<SavingsIncreaseDecreaseViewModel>>
{

    public string SavingsAccountId { get; set; }    
    public decimal Amount { get; set; }
    public ContributionChangeRequest ContributionChangeRequest { get; set; }

}


