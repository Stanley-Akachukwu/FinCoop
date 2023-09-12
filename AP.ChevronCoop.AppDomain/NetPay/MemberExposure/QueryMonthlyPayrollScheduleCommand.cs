using System;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.NetPay.MemberExposure
{
    public class MonthlyPayrollScheduleCommand : IRequest<MonthlyPayrollScheduleViewModel>
    {
        public MonthlyPayrollScheduleCommand()
        {
        }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}

