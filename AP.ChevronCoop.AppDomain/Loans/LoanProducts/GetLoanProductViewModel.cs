using System.Text.Json;
using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.AppDomain.Loans.LoanProductCharges;
using AP.ChevronCoop.Entities.Loans.LoanProducts;

namespace AP.ChevronCoop.AppDomain.Loans.LoanProducts;

public class GetLoanProductViewModel : LoanProductMasterView
{
  public List<string> SavingsOffSets { get; set; }
  public List<string> MemberTypes { get; set; }

  public string CurrencyName { get; set; }
  public string CurrencySymbol { get; set; }
  public string ApprovalWorkFlowName { get; set; }
  public List<LedgerAccountViewModel> LedgerAccounts { get; set; }
  public List<LedgerAccountViewModel> AssetLedgerAccounts { get; set; }
  public List<LedgerAccountViewModel> ExpenseLedgerAccounts { get; set; }

  public List<ChargeLookup> AdminOffsetCharges { get; set; }
  public List<ChargeLookup> DisbursementCharges { get; set; }
  public List<ChargeLookup> WaitingPeriodCharges { get; set; }
  public List<ChargeLookup> TopUpCharges { get; set; }
  public List<ChargeLookup> WaivedCharges { get; set; }
}

public class GetUserLoanProductViewModel : LoanProductViewModel
{
  public List<string> SavingsOffSets => JsonSerializer.Deserialize<List<string>>(SavingsOffSetJson);
  public List<string> MemberTypes => JsonSerializer.Deserialize<List<string>>(MemberTypeJson);

  public string CurrencyName { get; set; }
  public string CurrencySymbol { get; set; }
  public List<ChargeLookup> AdminCharges { get; set; }
  public List<ChargeLookup> WaitingPeriodCharges { get; set; }
  public List<ChargeLookup> TopUpCharges { get; set; }
  public List<ChargeLookup> WaivedCharges { get; set; }
}