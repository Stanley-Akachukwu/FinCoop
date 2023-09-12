using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
public partial class CreateSavingsCashAdditionCommand : CreateCommand, IRequest<CommandResult<SavingsCashAdditionViewModel>>
{

    public string  CustomerId { get; set; }
    public string SavingsAccountId { get; set; }

    public decimal Amount { get; set; }

    public DepositFundingSourceType ModeOfPayment { get; set; }

    public string ModeOfPaymentAccountId { get; set; }

    public string Document { get; set; }
    public string MimeType { get; set; }
    public string FileName { get; set; }
    public int FileSize { get; set; }

   

}


