using System;
namespace AP.ChevronCoop.AppDomain.NetPay.MemberExposure
{
    public class MonthlyPayrollScheduleViewModel
    {
        public MonthlyPayrollScheduleViewModel()
        {
            Data = new HashSet<PayrollScheduleDataViewModel>();
        }
        public bool Error { get; set; }
        public ICollection<PayrollScheduleDataViewModel> Data { get; set; }

    }
}

