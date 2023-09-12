using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanAccounts;

public class UpdateLoanAccountCommand : UpdateCommand, IRequest<CommandResult<LoanAccountViewModel>>
{
  public string AccountNo { get; set; }

  public string LoanApplicationId { get; set; }

  public string CustomerId { get; set; }

  public string PrincipalBalanceAccountId { get; set; }

  public string PrincipalLossAccountId { get; set; }

  public string InterestBalanceAccountId { get; set; }

  public string InterestLossAccountId { get; set; }

  public string ChargesPayableAccountId { get; set; }

  public decimal Principal { get; set; }

  public string TenureUnit { get; set; }

  public decimal TenureValue { get; set; }

  public DateTimeOffset RepaymentCommencementDate { get; set; }

  public bool UseSpecialDeposit { get; set; }

  public string SpecialDepositId { get; set; }

  public string DestinationAccountId { get; set; }

  public bool IsClosed { get; set; }

  public string ClosedByUserId { get; set; }

  public string LoanTopupId { get; set; }
}