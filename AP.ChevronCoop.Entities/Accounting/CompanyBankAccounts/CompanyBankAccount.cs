using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.MasterData.Banks;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts
{

    //2 UBA accounts
    //3 GTB accounts  GT1=100,000,0000, issue 1m, 99,000,000
    //4 Access accounts
    public class CompanyBankAccount : BaseEntity<string>
    {

        public CompanyBankAccount()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        
        public virtual LedgerAccount LedgerAccount { get; set; } //100,000,000

        public string LedgerAccountId { get; set; }

        public string BankId { get; set; }

        public virtual Bank Bank { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }
        
        public string CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string BVN { get; set; }

        [NotMapped]
        public decimal? ClearedBalance
        {
            get
            {
                return LedgerAccount?.ClearedBalance;
            }
        }


        [NotMapped]
        public decimal? UnClearedBalance
        {
            get
            {
                return LedgerAccount?.UnclearedBalance;
            }
        }

        [NotMapped]
        public decimal? AvailableBalance
        {
            get
            {
                return LedgerAccount?.AvailableBalance;
            }
        }


        public override string DisplayCaption
        {
            get
            {
                return "";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }

    }
}