using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.Payroll;
using System.Globalization;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;


namespace AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations
{
    public class PayrollCronJobConfig : BaseEntity<string>  // This optional
    {

        public PayrollCronJobConfig()
        {
            Id = NUlid.Ulid.NewUlid().ToString();
        }


        public virtual PayrollDeductionSchedule DeductionSchedule { get; set; } //100,000,000
        public string DeductionScheduleId { get; set; }

        public CronJobType CronJobType { get; set; } // Payroll Deduction | Interest computation 

        public string JobName { get; set; }
        public DateTime JobDate { get; set; }
        public CronJobStatus JobStatus { get; set; }

        public DateTime ComputationStartDate { get; set; }
        public DateTime ComputationEndDate { get; set; }

        public int RecordsProcessed { get; set; }// number of items in the schedules
        public decimal TotalAmount { get; set; }
        public long TotalCount { get; set; }
        //idempotent, idempotency

        public override string DisplayCaption { get; }
        public override string DropdownCaption { get; }
        public override string ShortCaption { get; }
    }


}