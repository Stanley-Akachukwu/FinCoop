using System;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.NetPay.MemberExposure
{
    public class CreateMemberExposureCommand : IRequest<MemberExposureViewModel>
    {
        public string EmployeeNo { get; set; }
        /// <summary>
        ///  Int,  the index of the month being called. 1 being Jan and 12 being december
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// Int, the year being called
        /// </summary>
        public int Year { get; set; }
    }
}

