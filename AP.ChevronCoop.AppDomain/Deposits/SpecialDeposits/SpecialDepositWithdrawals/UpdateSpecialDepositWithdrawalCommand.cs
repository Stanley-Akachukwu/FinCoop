using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public partial class UpdateSpecialDepositWithdrawalCommand :  IRequest<CommandResult<SpecialDepositWithdrawalViewModel>>
    {
        public string Id { get; set; }
        public string SpecialDepositSourceAccountId { get; set; }
        public decimal Amount { get; set; }
        public WithdrawalAccountType WithdrawalDestinationType { get; set; }
        public string CustomerDestinationBankAccountId { get; set; }
        public string TransactionJournalId { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string CreatedByUserId { get; set; }

    }

}

