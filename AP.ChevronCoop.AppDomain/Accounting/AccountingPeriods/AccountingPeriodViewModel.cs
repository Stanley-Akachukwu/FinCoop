using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods
{
    public partial class AccountingPeriodViewModel : BaseViewModel
    {

        [MaxLength(900)]
        [Required]
        public string CalendarId { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }


        [Required]
        public DateTime StartDate { get; set; }


        [Required]
        public DateTime EndDate { get; set; }


        [Required]
        public bool IsCurrent { get; set; }


        [Required]
        public bool IsClosed { get; set; }

        public string ClosedByUserName { get; set; }

        public DateTime? DateClosed { get; set; }
        [MaxLength(256)]
        public string CreatedByUserId { get; set; }
        [MaxLength(256)]
        public string UpdatedByUserId { get; set; }
        [MaxLength(256)]
        public string DeletedByUserId { get; set; }

    }





}
