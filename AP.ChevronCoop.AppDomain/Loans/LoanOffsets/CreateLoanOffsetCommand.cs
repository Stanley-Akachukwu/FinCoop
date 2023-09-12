using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Loans.LoanOffsets;

public class CreateLoanOffsetCommand : CreateCommand, IRequest<CommandResult<LoanOffsetViewModel>>
{
    public string LoanAccountId { get; set; }

    public decimal OffsetAmount { get; set; }

    public decimal PrincipalBalance { get; set; }

    public decimal InterestBalance { get; set; }

    // public decimal TotalOffsetCharges { get; set; }

    public AllowedOffsetType AllowedOffsetType { get; set; }

    public LoanRepaymentMode LoanRepaymentMode { get; set; }

    public DateTimeOffset OffSetRepaymentDate { get; set; }

    public string SavingsAccountId { get; set; }
    public List<string>? RepaymentSchedules { get; set; }

    public string SpecialDepositAccountId { get; set; }

    public DateTime DeductionStartAfter { get; set; }

    public DateTime OffsetToBeCalculatedAfter { get; set; }

    public string CustomerPaymentDocumentId { get; set; }
}