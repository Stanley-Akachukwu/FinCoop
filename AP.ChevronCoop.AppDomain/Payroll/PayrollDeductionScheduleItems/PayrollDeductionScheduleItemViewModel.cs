using AP.ChevronCoop.AppDomain;
using System.ComponentModel.DataAnnotations;

public class PayrollDeductionScheduleItemViewModel : BaseViewModel
{
    [MaxLength(80)]
    [Required]
    public string PayrollDeductionScheduleId { get; set; }


    [Required]
    public string BatchRefNo { get; set; }


    [Required]
    public string MemberId { get; set; }


    [Required]
    public string MemberName { get; set; }


    [Required]
    public string AccountNo { get; set; }


    [Required]
    public decimal Amount { get; set; }

    [MaxLength(64)]
    [Required]
    public string PayrollCode { get; set; }

    [MaxLength(480)]
    [Required]
    public string Narration { get; set; }


    [Required]
    public DateTime PayrollDate { get; set; }


    [Required]
    public DateTime AccountDueDate { get; set; }

    [MaxLength(240)]
    [Required]
    public string CurrentStatus { get; set; }

    [MaxLength(64)]
    [Required]
    public string DeductionType { get; set; }
    [MaxLength(80)]
    public string? LoanRepaymentScheduleId { get; set; }
    [MaxLength(80)]
    public string? SavingsAccountDeductionScheduleId { get; set; }
    [MaxLength(80)]
    public string? SpecialDepositAccountDeductionScheduleId { get; set; }
}