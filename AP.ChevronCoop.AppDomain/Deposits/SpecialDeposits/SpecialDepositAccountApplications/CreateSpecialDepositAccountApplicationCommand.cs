
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public partial class CreateSpecialDepositAccountApplicationCommand : IRequest<CommandResult<SpecialDepositAccountApplicationViewModel>>
    {
       
        public string CustomerId { get; set; }
        public string DepositProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public string PaymentAccountNumber { get; set; }
        public string PaymentBankName { get; set; }
        public DepositFundingSourceType ModeOfPayment { get; set; }
        public string Document { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string CreatedByUserId { get; set; }

    }
}

