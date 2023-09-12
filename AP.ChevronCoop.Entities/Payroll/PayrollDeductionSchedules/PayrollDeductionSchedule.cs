using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;

namespace AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;

public class PayrollDeductionSchedule : BaseEntity<string>
{
  public PayrollDeductionSchedule()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }

  public string ScheduleName { get; set; }

  public PayrollScheduleType ScheduleType { get; set; }

  public string? BankAccountId { get; set; }
  public virtual CompanyBankAccount? BankAccount { get; set; }

  public string? SpecialDepositBankAccountId { get; set; }
  public virtual CompanyBankAccount? SpecialDepositBankAccount { get; set; }


  public string? FixedDepositBankAccountId { get; set; }
  public virtual CompanyBankAccount? FixedDepositBankAccount { get; set; }


  public int DeductionsCount { get; set; }
  public decimal TotalDeductions { get; set; }

  public int MinDecimalPlace { get; set; } = 1;
  public int MaxDecimalPlace { get; set; } = 1;

  public DateTime AdviseDate { get; set; }
  public DateTime ExpectedDate { get; set; }

  public bool IsPosted { get; set; }
  public DateTime PayrollDate { get; set; }

  public bool IsUploaded { get; set; }
  public DateTime LastUploadedDate { get; set; }

  public bool IsProcessed { get; set; }
  public DateTime ProcessedDate { get; set; }

  //generation deduction schedules, process uploaded deduction files
  //jobs will be triggered by actions from the UI
  public CronJobStatus GenerateDeductionCronJobStatus { get; set; }
  public DateTime? GenerateDeductionCronJobStartedDate { get; set; }
  public DateTime? GenerateDeductionCronJobCompletedDate { get; set; }

  public CronJobStatus ProcessDeductionCronJobStatus { get; set; }
  public DateTime? ProcessDeductionCronJobStartedDate { get; set; }
  public DateTime? ProcessDeductionCronJobCompletedDate { get; set; }


  public virtual List<PayrollCronJobConfig> PayrollCronJobs { get; set; } = new(); //100 items
  public virtual List<PayrollDeductionScheduleItem> ScheduleItems { get; set; } = new(); //100 items
  public virtual List<PayrollDeductionItem> DeductionItems { get; set; } = new(); //90 items or 120 items

  //public List<PayrollDeductionItem> ValidDeductionItems { get; set; } //100 items 
  //public List<PayrollDeductionItem> InvalidDeductionItems { get; set; } //10 items 
  //public List<PayrollDeductionItem> MissingDeductionItems { get; set; } //10 items 


  public override string DisplayCaption { get; }
  public override string DropdownCaption { get; }
  public override string ShortCaption { get; }

  //action menus
  // Generate schedule, Regenerate schedule

  public void GenerateSchedule()
  {
  }

  //to discuss further
  public void RegenerateSchedule()
  {
  }


  //generate initial journal entries for payroll deductions
  public void PostToPayroll()
  {
  }


  //generate final journal entries
  public void UploadPayrollAdvise(List<PayrollDeductionItem> deductionItems)
  {
  }
}