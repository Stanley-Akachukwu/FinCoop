using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.MasterData.Banks;

namespace AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;

//1 Wema account
// 1 Stanbic account
public class CustomerBankAccount : BaseEntity<string>
{
  public CustomerBankAccount()
  {
    Id = NUlid.Ulid.NewUlid().ToString();
  }

  public virtual LedgerAccount LedgerAccount { get; set; } //100,000,000
  public string LedgerAccountId { get; set; }

  public string CustomerId { get; set; }

  public virtual Customer Customer { get; set; }

  public string BankId { get; set; }

  public virtual Bank Bank { get; set; }

  public string AccountName { get; set; }

  public string AccountNumber { get; set; }

  public string BVN { get; set; }

  public string Branch { get; set; }

  public override string DisplayCaption => "";

  public override string DropdownCaption => "";

  public override string ShortCaption => "";
}