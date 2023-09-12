using AP.ChevronCoop.AppDomain;
using AP.ChevronCoop.Entities;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication
{
    public class CreateFixedDepositAccountApplicationCommandFE : CreateCommand
    {
        public string CustomerId { get; set; }

        public string DepositProductId { get; set; }

        public decimal Amount { get; set; }

        public decimal InterestRate { get; set; }

        public MaturityInstructionType MaturityInstructionType { get; set; }

        public WithdrawalAccountType LiquidationAccountType { get; set; }

        public string LiquidationAccountId { get; set; }

        public DepositFundingSourceType ModeOfPayment { get; set; }

        public string ModeOfPaymentAccountId { get; set; }

        public string Document { get; set; }

        public string MimeType { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public string ApprovalId { get; set; }
    }
}
