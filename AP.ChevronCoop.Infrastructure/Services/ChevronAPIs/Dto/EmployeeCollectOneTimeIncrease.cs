using System;
using Newtonsoft.Json;

namespace AP.ChevronCoop.Infrastructure.Services.ChevronAPIs.Dto
{
    public class EmployeeCollectOneTimeIncrease
    {


        [JsonProperty("empno")]
        public long EmployeeNo { get; set; }

        /// <summary>
        /// The current monthly contribution to savings wallet
        /// </summary>
        [JsonProperty("currentDedution")]
        public long CurrentDedution { get; set; }
        /// <summary>
        /// additional contribution in view
        /// </summary>
        [JsonProperty("additionalDeduction")]
        public long AdditionalDeduction { get; set; }
        //The month the contribution wants to be taken, 1 being Jan and 12 being December
        [JsonProperty("month")]
        public int Month { get; set; }
        /// <summary>
        /// The year the contribution wants to be taken.
        /// </summary>
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }

    }
}

