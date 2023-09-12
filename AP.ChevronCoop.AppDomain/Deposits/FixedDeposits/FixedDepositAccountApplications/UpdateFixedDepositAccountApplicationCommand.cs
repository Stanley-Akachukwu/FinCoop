using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
public partial class UpdateFixedDepositAccountApplicationCommand : UpdateCommand, IRequest<CommandResult<FixedDepositAccountApplicationViewModel>>
{

    public string ApplicationNo { get; set; }
    public string CustomerId { get; set; }
    public string DepositProductId { get; set; }
    public decimal Amount { get; set; }
    public string TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public decimal InterestRate { get; set; }
    public string MaturityInstructionType { get; set; }
    public string LiquidationAccountType { get; set; }
    public string SavingsLiquidationAccountId { get; set; }
    public string SpecialDepositLiquidationAccountId { get; set; }
    public string CustomerBankLiquidationAccountId { get; set; }
    public string ModeOfPayment { get; set; }
    public string SpecialDepositFundingSourceAccountId { get; set; }
    public string CustomerBankFundingSourceAccountId { get; set; }
    public string PaymentDocumentId { get; set; }
    public string ApprovalId { get; set; }
}


