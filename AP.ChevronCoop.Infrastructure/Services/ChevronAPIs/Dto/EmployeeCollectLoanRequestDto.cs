using System;
using Newtonsoft.Json;

namespace AP.ChevronCoop.Infrastructure.Services.ChevronAPIs.Dto
{
    public class EmployeeCollectLoanRequestDto
    {

        [JsonProperty("empno")]
        public string EmployeeNo { get; set; }
        /// <summary>
        /// Employee month exposure including his contributions and repayments.
        /// </summary>
        [JsonProperty("currentExposure")]
        public long CurrentMonthlyExposure { get; set; }
        [JsonProperty("monthlyRepayment")]
        public long MonthlyRepayment { get; set; }
        /// <summary>
        /// The month the repayment will start from 1 being Jan and 12 being december.
        /// Please note once cutover has been run the repayment start month becomes the next month
        /// </summary>
        [JsonProperty("repayStartMonth")]
        public long RepayStartMonth { get; set; }
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("voucher")]
        public string Voucher { get; set; }


    }
}

