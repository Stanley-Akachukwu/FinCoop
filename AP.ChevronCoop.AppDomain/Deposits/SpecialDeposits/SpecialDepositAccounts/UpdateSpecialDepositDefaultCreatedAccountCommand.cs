using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccounts
{
    public partial class UpdateSpecialDepositDefaultCreatedAccountCommand : IRequest<CommandResult<SpecialDepositAccountViewModel>>
    {
        public decimal FundingAmount { get; set; }
        public DepositFundingSourceType ModeOfPayment { get; set; } = DepositFundingSourceType.PAYROLL;
        //public string Document { get; set; }
        //public string MimeType { get; set; }
        //public string FileName { get; set; }
        //public int FileSize { get; set; }
        public string SpecialDepositAccountId { get; set; }
        public string PaymentAccountNumber { get; set; }
        public string PaymentBankName { get; set; }
        public string UpdatedByUserId { get; set; }
    }
}


