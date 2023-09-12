using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;

namespace AP.ChevronCoop.Entities.Payroll;

public class PayrollDeductionItem : BaseEntity<string>
{
    public PayrollDeductionItem()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }

    public string PayrollDeductionScheduleId { get; set; }
    public PayrollDeductionSchedule PayrollDeductionSchedule { get; set; }

    public string BatchRefNo { get; set; } // prefix for saving SA, SD FD , LA456790

    public string MemberId { get; set; }
    public string EmployeeNo { get; set; }
    public string MemberName { get; set; }
    public string AccountNo { get; set; }
    public decimal Amount { get; set; }
    public string PayrollCode { get; set; }
    public string Narration { get; set; }
    public DateTime PayrollDate { get; set; }
    public string CurrentStatus { get; set; }
    public DateTime AccountDueDate { get; set; }

    public PayrollDeductionType DeductionType { get; set; }

    public decimal? TotalDeduction { get; set; }

    // public List<PayrollErrorType> PayrollErrors { get; set; } = new();
    // public List<string> PayrollErrors { get; set; } = new();

    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}
