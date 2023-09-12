using System;
using Newtonsoft.Json;

namespace AP.ChevronCoop.Infrastructure.Services.ChevronAPIs.Dto
{
	public class EmployeeCollectLoanResponseDto
	{
		public EmployeeCollectLoanResponseDto()
		{
        }

        /// <summary>
        ///  This response is false if there was no error
        /// </summary>
        [JsonProperty("error")]
        public bool Error { get; set; }
        /// <summary>
        /// //This value can be 1 or 0. ! signifying true meaning the member can take the loan and 0 signifying false the member cannot take the loan
        /// </summary>
        [JsonProperty("value")]
        public short Value { get; set; }
    }
}

