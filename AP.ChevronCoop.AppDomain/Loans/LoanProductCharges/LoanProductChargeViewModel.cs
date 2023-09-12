using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;

public class LoanProductChargeViewModel : BaseViewModel
{
  public string ProductId { get; set; }

  public string ChargeType { get; set; }

  public string ChargeId { get; set; }

  public string CreatedByUserId { get; set; }

  public string UpdatedByUserId { get; set; }

  public string DeletedByUserId { get; set; }
}