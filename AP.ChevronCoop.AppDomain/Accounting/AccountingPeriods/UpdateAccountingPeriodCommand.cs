using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AP.ChevronCoop.AppDomain.Accounting.AccountingPeriods
{
    public partial class UpdateAccountingPeriodCommand : UpdateCommand, IRequest<CommandResult<AccountingPeriodViewModel>>
    {

        [MaxLength(40)]
        [Required]
        public string CalendarId { get; set; }

        [MaxLength(128)]
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
      

    }







}
