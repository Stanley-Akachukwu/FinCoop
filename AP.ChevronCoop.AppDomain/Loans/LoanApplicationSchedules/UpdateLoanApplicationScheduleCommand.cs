using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;

public class UpdateLoanApplicationScheduleCommand : UpdateCommand,
  IRequest<CommandResult<LoanApplicationScheduleViewModel>>
{
  [MaxLength(80)] [Required] public string LoanApplicationId { get; set; }

  [Required] public int RepaymentNo { get; set; }

  [Required] public DateTime RepaymentDate { get; set; }

  [MaxLength(64)] [Required] public string TenureUnit { get; set; }

  [Required] public decimal PeriodPayment { get; set; }

  [Required] public decimal PeriodPrincipal { get; set; }

  [Required] public decimal PeriodInterest { get; set; }


  public decimal? InterestBalance { get; set; }


  public decimal? PrincipalBalance { get; set; }


  public decimal? TotalBalance { get; set; }
}