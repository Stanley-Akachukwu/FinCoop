using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers
{
    public partial class CreateSpecialDepositFundTransferCommand :  IRequest<CommandResult<SpecialDepositFundTransferViewModel>>
    {
        public string SpecialDepositAccountId { get; set; }
        public decimal Amount { get; set; }
        public DestinationAccountType DestinationAccountType { get; set; }
        public string? SavingAccountDestinationId { get; set; }
        public string? FixedDepositDestinationAccountId { get; set; }
        public string CreatedByUserId { get; set; }

    }

}

