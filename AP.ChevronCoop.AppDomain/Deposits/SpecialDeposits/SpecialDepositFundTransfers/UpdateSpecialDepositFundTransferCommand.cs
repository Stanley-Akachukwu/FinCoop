using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public partial class UpdateSpecialDepositFundTransferCommand :  IRequest<CommandResult<SpecialDepositFundTransferViewModel>>
    {
        public string Id { get; set; }
        public string SpecialDepositAccountId { get; set; }
        public decimal Amount { get; set; }
        public DestinationAccountType DestinationAccountType { get; set; }
        public string SavingAccountDestinationId { get; set; }
        public string FixedDepositDestinationAccountId { get; set; }
        public string TransactionJournalId { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string CreatedByUserId { get; set; }

    }
}
 
 
