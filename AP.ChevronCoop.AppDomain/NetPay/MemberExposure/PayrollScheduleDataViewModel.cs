using System;
namespace AP.ChevronCoop.AppDomain.NetPay.MemberExposure
{
    public class PayrollScheduleDataViewModel
    {
        public PayrollScheduleDataViewModel()
        {
        }

        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string DbaCode { get; set; }
        public decimal DeductionAmount { get; set; }
        public string Voucher { get; set; }
    }
}

