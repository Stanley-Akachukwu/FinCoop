using AP.ChevronCoop.Entities.Accounting.FinancialCalendars;

namespace AP.ChevronCoop.Entities.Accounting.AccountingPeriods
{
    public class AccountingPeriod : BaseEntity<string>
    {
        public AccountingPeriod()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }
        
        public virtual FinancialCalendar Calendar { get; set; }
        
        public string CalendarId { get; set; }
        
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsCurrent { get; set; } = false;
        
        public bool IsClosed { get; set; } = false;

        public string ClosedByUserName { get; set; }

        public DateTime? DateClosed { get; set; }


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