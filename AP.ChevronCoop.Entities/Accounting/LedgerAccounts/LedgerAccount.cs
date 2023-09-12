using AP.ChevronCoop.Entities.MasterData.Currencies;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.Accounting.LedgerAccounts
{
    public class LedgerAccount : BaseEntity<string>
    {


        private string name;
        private string code;
        private LedgerAccount parent;
        private IList<LedgerAccount> children;



        public LedgerAccount()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
            Children = new List<LedgerAccount>();
            ClearedBalance = 0;
            UnclearedBalance = 0;
            AvailableBalance = 0;
            LedgerBalance = 0;
        }


        public COAType AccountType { get; set; } = COAType.ASSET;

        public LedgerBalanceUOM UOM { get; set; } = LedgerBalanceUOM.CURRENCY;
        public virtual Currency Currency { get; set; }
        public string CurrencyId { get; set; }
        public string Code
        { 
            get; set;
        }


        public string Name { get; set; }

        public string ParentId { get; set; }

        public virtual LedgerAccount Parent { get; set; }

        [InverseProperty(nameof(Parent))]
        public virtual List<LedgerAccount> Children { get; set; }


        [NotMapped, Browsable(false)]
        public bool IsValid
        {
            get
            {
                LedgerAccount currentObj = Parent;
                while (currentObj != null)
                {
                    if (currentObj == this)
                    {
                        return false;
                    }
                    currentObj = currentObj.Parent;
                }
                return true;
            }
        }

        public decimal ClearedBalance { get; set; } = 0;

        public decimal UnclearedBalance { get; set; } = 0;
        public decimal LedgerBalance { get; set; } = 0;
        public decimal AvailableBalance { get; set; } = 0;

        public bool IsOfficeAccount { get; set; } = false;

        public bool AllowManualEntry { get; set; } = true;

        [Required]
        public bool IsClosed { get; set; } = false;

        public DateTime? DateClosed { get; set; }

        public string ClosedByUserName { get; set; }


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