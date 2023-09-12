using AP.ChevronCoop.AppDomain;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
public partial class FixedDepositChangeInMaturityViewModel : BaseViewModel
{

    public string MaturityInstructionType { get; set; }
    public string FixedDepositAccountId { get; set; }
    public string LiquidationAccountType { get; set; }
    public string SavingsLiquidationAccountId { get; set; }
    public string SpecialDepositLiquidationAccountId { get; set; }
    public string CustomerBankLiquidationAccountId { get; set; }
    public string ApprovalId { get; set; }

}


