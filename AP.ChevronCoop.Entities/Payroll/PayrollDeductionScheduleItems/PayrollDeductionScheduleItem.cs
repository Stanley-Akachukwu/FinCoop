using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;

namespace AP.ChevronCoop.Entities.Payroll;

//LoanDeductionSchedule->ScheduleItems->DeductionItems
//SavingsDeductionSchedule ->ScheduleItems->DeductionItems
//SpecialDepositsDeductionSchedule-> ScheduleItems->DeductionItems
//FixedDepositsDeductionSchedule-> ScheduleItems->DeductionItems


public class PayrollDeductionScheduleItem : BaseEntity<string>
{
    public PayrollDeductionScheduleItem()
    {
        Id = NUlid.Ulid.NewUlid().ToString();
    }


    public string PayrollDeductionScheduleId { get; set; }
    public PayrollDeductionSchedule PayrollDeductionSchedule { get; set; }

    public string BatchRefNo { get; set; }

    public string MemberId { get; set; }
    public string MemberName { get; set; }
    public string AccountNo { get; set; }
    public decimal OriginalAmount { get; set; }
    public decimal Amount { get; set; }
    public string PayrollCode { get; set; } // Get from deposit application
    public string Narration { get; set; } // product name
    public DateTime PayrollDate { get; set; } // When payroll should run
    public DateTime AccountDueDate { get; set; }
    public string CurrentStatus { get; set; }

    public PayrollDeductionType DeductionType { get; set; }

    public string? LoanRepaymentScheduleId { get; set; }
    public virtual LoanRepaymentSchedule? LoanRepaymentSchedule { get; set; }

    public string? SavingsAccountDeductionScheduleId { get; set; }
    public virtual SavingsAccountDeductionSchedule? SavingsAccountDeductionSchedule { get; set; }

    public string? PayrollCronJobConfigId { get; set; }
    public virtual PayrollCronJobConfig? PayrollCronJobConfig { get; set; }


    public string? SpecialDepositAccountDeductionScheduleId { get; set; }
    public virtual SpecialDepositAccountDeductionSchedule? SpecialDepositAccountDeductionSchedule { get; set; }
    //public decimal? TotalDeduction { get; set; }


    public override string DisplayCaption { get; }
    public override string DropdownCaption { get; }
    public override string ShortCaption { get; }
}
