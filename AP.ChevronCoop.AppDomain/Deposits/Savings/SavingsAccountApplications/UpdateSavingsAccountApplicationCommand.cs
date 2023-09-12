using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
public partial class UpdateSavingsAccountApplicationCommand : UpdateCommand, IRequest<CommandResult<SavingsAccountApplicationViewModel>>
{

    public string ApplicationNo { get; set; }

    public string CustomerId { get; set; }

    public string DepositProductId { get; set; }

    public string ApprovalId { get; set; }
    public decimal Amount { get; set; }

}


