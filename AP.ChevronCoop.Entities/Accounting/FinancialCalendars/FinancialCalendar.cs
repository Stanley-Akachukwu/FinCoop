using System.ComponentModel.DataAnnotations.Schema;
using AP.ChevronCoop.Entities.Accounting.AccountingPeriods;

namespace AP.ChevronCoop.Entities.Accounting.FinancialCalendars
{
    public class FinancialCalendar : BaseEntity<string>
    {

        public FinancialCalendar()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsCurrent { get; set; }


        public bool IsClosed { get; set; }
        public string ClosedByUserName { get; set; }
        public DateTime? DateClosed { get; set; }

        public virtual List<AccountingPeriod> AccountingPeriods { get; set; }


        public override string DisplayCaption
        {
            get
            {
                return $"{Name}";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return $"{Name}";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return $"{Name}";
            }
        }
    }
}