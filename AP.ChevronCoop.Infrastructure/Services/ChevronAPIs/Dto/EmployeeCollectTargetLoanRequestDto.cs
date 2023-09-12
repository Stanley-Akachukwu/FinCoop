using System;
using Newtonsoft.Json;

namespace AP.ChevronCoop.Infrastructure.Services.ChevronAPIs.Dto
{
    public class EmployeeCollectTargetLoanRequestDto
    {

        [JsonProperty("empno")]
        public long EmployeeNo { get; set; }
        /// <summary>
        /// The target loan type being applied for.i.e. benefit code of the target loan
        /// </summary>
        [JsonProperty("targetLoanType")]
        public long TargetLoanType { get; set; }
        /// <summary>
        /// The persons current exposure for the month being applied for
        /// </summary>
        [JsonProperty("currentDeductionForSpecifiedMonth")]
        public long CurrentDeductionForSpecifiedMonth { get; set; }
        /// <summary>
        /// The loan amount being applied for
        /// </summary>
        [JsonProperty("targetAmount")]
        public long TargetAmount { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}

