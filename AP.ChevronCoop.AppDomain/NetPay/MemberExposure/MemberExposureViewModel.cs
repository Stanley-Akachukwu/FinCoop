using System;
namespace AP.ChevronCoop.AppDomain.NetPay.MemberExposure
{
    public class MemberExposureViewModel
    {
        public MemberExposureViewModel()
        {
        }


        public bool Error { get; set; }
        public decimal Value { get; set; }
        public string Message { get; set; }
    }
}

