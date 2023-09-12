using System;
using Newtonsoft.Json;

namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto
{
	public class EmailRequest
	{
		[JsonProperty("to")]
		public string to { get; set; }
        [JsonProperty(nameof(subject))]
        public string subject { get; set; }
        [JsonProperty("html")]
        public string html { get; set; }
    }
}

