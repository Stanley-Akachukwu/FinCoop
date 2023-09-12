
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals
{
    public partial class SpecialDepositWithdrawalViewModel : BaseViewModel
    {
        [MaxLength(80)]
        public string? SpecialDepositSourceAccountId { get; set; }


        [Required]
        public decimal Amount { get; set; }


        [Required]
        public string WithdrawalDestinationType { get; set; }
        [MaxLength(80)]
        public string? CustomerDestinationBankAccountId { get; set; }
        [MaxLength(80)]
        public string? TransactionJournalId { get; set; }


        [Required]
        public bool IsProcessed { get; set; }

        public DateTime? ProcessedDate { get; set; }
        [MaxLength(80)]
        public string? ApprovalId { get; set; }

    }




}

