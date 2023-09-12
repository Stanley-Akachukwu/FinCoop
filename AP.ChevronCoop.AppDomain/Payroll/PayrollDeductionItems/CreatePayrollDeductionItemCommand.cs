using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;

public class CreatePayrollDeductionItemCommand : CreateCommand, IRequest<CommandResult<PayrollDeductionItemViewModel>>
{
    [MaxLength(80)]
    [Required]
    public string PayrollDeductionScheduleId { get; set; }


    public string? BatchRefNo { get; set; }

    [MaxLength(80)]
    [Required]
    public string MemberId { get; set; }


    public string? EmployeeNo { get; set; }

    [MaxLength(240)]
    [Required]
    public string MemberName { get; set; }

    [MaxLength(64)]
    [Required]
    public string AccountNo { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [MaxLength(510)]
    [Required]
    public string PayrollCode { get; set; }

    [MaxLength(510)]
    [Required]
    public string Narration { get; set; }

    [Required]
    public DateTime PayrollDate { get; set; }

    [MaxLength(64)]

    public string? CurrentStatus { get; set; }

    [Required]
    public DateTime AccountDueDate { get; set; }

    [MaxLength(64)]
    [Required]
    public string DeductionType { get; set; }


   // public decimal? TotalDeduction { get; set; }

}