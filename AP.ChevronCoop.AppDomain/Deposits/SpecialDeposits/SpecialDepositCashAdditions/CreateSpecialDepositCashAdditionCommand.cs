using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions
{
    public partial class CreateSpecialDepositCashAdditionCommand : IRequest<CommandResult<SpecialDepositCashAdditionViewModel>>
    {
        public string SpecialDepositAccountId { get; set; }
        public decimal Amount { get; set; }
     //   public string CustomerPaymentDocumentId { get; set; }

       // public string BatchRefNo { get; set; }

       // public string TransactionJournalId { get; set; }
      //  public bool IsProcessed { get; set; }
       // public DateTime? ProcessedDate { get; set; }

        public DepositFundingSourceType ModeOfPayment { get; set; } = DepositFundingSourceType.BANK_TRANSFER;
        public string Document { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string CreatedByUserId { get; set; }





    }
}
 
 
