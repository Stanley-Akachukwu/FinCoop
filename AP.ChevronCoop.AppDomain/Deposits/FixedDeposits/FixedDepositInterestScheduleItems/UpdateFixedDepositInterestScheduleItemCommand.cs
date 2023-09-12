using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositInterestScheduleItems;
public partial class UpdateFixedDepositInterestScheduleItemCommand : UpdateCommand, IRequest<CommandResult<FixedDepositInterestScheduleItemViewModel>>
{

   
    public string FixedDepositAccountId { get; set; }

    public string FixedDepositInterestScheduleId { get; set; }
    
    public decimal OldBalance { get; set; }
    
    public decimal PeriodCashAddition { get; set; }
    
    public decimal InterestRate { get; set; }
    
    public decimal InterestEarned { get; set; }
    
    public decimal NewBalance { get; set; }
    public string FixedDepositInterestScheduleId1 { get; set; }

}


