using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;


namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
public partial class UpdateSavingsCashAdditionCommand : UpdateCommand, IRequest<CommandResult<SavingsCashAdditionViewModel>>
{


    public string SavingsAccountId { get; set; }

    public decimal Amount { get; set; }

    public  DepositFundingSourceType ModeOfPayment { get; set; }

    public string CustomerPaymentDocumentId { get; set; }


}


