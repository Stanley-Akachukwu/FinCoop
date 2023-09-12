
using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications
{
    public partial class UpdateSpecialDepositAccountApplicationCommand : UpdateCommand, IRequest<CommandResult<SpecialDepositAccountApplicationViewModel>>
    {

        public string ApplicationNo { get; set; }
        public string CustomerId { get; set; }

        public string DepositProductId { get; set; }

        public string ApprovalId { get; set; }

        public decimal Amount { get; set; }

        public decimal InterestRate { get; set; }

        public string ModeOfPayment { get; set; }

        public string PaymentDocumentId { get; set; }

        public string PaymentAccountNumber { get; set; }

        public string PaymentBankName { get; set; }

    }
}

