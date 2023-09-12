using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
public partial class CreateFixedDepositAccountApplicationCommand : CreateCommand, IRequest<CommandResult<FixedDepositAccountApplicationViewModel>>
{

    public string CustomerId { get; set; }
    public string DepositProductId { get; set; }
    public decimal Amount { get; set; }

    public decimal InterestRate { get; set; }
    public MaturityInstructionType MaturityInstructionType { get; set; }
    public WithdrawalAccountType LiquidationAccountType { get; set; }//savings, special, customer
    public string LiquidationAccountId { get; set; }
    public DepositFundingSourceType ModeOfPayment { get; set; } //special, Bank Transfer
    public string ModeOfPaymentAccountId { get; set; }
    public string Document { get; set; }
    public string MimeType { get; set; }
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public string ApprovalId {get; set; }

}


