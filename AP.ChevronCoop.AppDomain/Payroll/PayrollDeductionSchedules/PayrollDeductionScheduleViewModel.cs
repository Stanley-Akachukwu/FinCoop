using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;

public class PayrollDeductionScheduleViewModel : BaseViewModel
{
    [MaxLength(512)]
    [Required]
    public string ScheduleName { get; set; }
    [MaxLength(80)]
    public string? BankAccountId { get; set; }
    [MaxLength(80)]
    public string? SpecialDepositBankAccountId { get; set; }
    [MaxLength(80)]
    public string? FixedDepositBankAccountId { get; set; }


    [Required]
    public int DeductionsCount { get; set; }


    [Required]
    public decimal TotalDeductions { get; set; }


    [Required]
    public int MinDecimalPlace { get; set; }


    [Required]
    public int MaxDecimalPlace { get; set; }


    [Required]
    public DateTime AdviseDate { get; set; }


    [Required]
    public DateTime ExpectedDate { get; set; }


    [Required]
    public bool IsPosted { get; set; }


    [Required]
    public DateTime PayrollDate { get; set; }


    [Required]
    public bool IsUploaded { get; set; }


    [Required]
    public DateTime LastUploadedDate { get; set; }


    [Required]
    public bool IsProcessed { get; set; }


    [Required]
    public DateTime ProcessedDate { get; set; }


    [Required]
    public int GenerateDeductionCronJobStatus { get; set; }

    public DateTime? GenerateDeductionCronJobStartedDate { get; set; }

    public DateTime? GenerateDeductionCronJobCompletedDate { get; set; }


    [Required]
    public int ProcessDeductionCronJobStatus { get; set; }

    public DateTime? ProcessDeductionCronJobStartedDate { get; set; }

    public DateTime? ProcessDeductionCronJobCompletedDate { get; set; }


    [Required]
    public string ScheduleType { get; set; }
}