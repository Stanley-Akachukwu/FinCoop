using AP.ChevronCoop.AppDomain;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
public partial class FixedDepositLiquidationViewModel : BaseViewModel
{

    [MaxLength(80)]
    [Required]
    public string FixedDepositAccountId { get; set; }

    public int? LiquidationAccountType { get; set; }
    [MaxLength(80)]
    public string? SavingsLiquidationAccountId { get; set; }
    [MaxLength(80)]
    public string? SpecialDepositLiquidationAccountId { get; set; }
    [MaxLength(80)]
    public string? CustomerBankLiquidationAccountId { get; set; }
    [MaxLength(80)]
    public string? TransactionJournalId { get; set; }
    [MaxLength(80)]
    public string? ApprovalId { get; set; }

}





