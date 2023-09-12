using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
public partial class CreateSavingsAccountApplicationCommand : CreateCommand, IRequest<CommandResult<SavingsAccountApplicationViewModel>>
{


    public string CustomerId { get; set; }

    public string DepositProductId { get; set; }

    public decimal Amount { get; set; }

}


