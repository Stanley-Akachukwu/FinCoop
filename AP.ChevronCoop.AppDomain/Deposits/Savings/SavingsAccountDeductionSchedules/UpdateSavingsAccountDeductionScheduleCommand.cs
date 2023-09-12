using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountDeductionSchedules;
public partial class UpdateSavingsAccountDeductionScheduleCommand : UpdateCommand, IRequest<CommandResult<SavingsAccountDeductionScheduleViewModel>>
{



    public string SavingsAccountId { get; set; }

    public string BatchRefNo { get; set; }

    public string MemberId { get; set; }

    public string EmployeeNo { get; set; }

    public string MemberName { get; set; }

    public string AccountNo { get; set; }

    public decimal Amount { get; set; }

    public string PayrollCode { get; set; }

    public string Narration { get; set; }

    public DateTime DueDate { get; set; }
    public string CurrentStatus { get; set; }

}


