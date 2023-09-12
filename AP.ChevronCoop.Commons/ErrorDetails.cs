using System.Text.Json.Serialization;

namespace AP.ChevronCoop.Commons
{
    public class ErrorDetails
    {

        public ErrorDetails()
        {
            ValidationErrors = new List<ModelValidationError>();
        }

        [JsonPropertyName("code")]
        //[JsonProperty("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        //[JsonProperty("message")]
        public string Message { get; set; }

        [JsonPropertyName("exception")]
        //[JsonProperty("exception")]
        public string Exception { get; set; }


        [JsonPropertyName("validationErrors")]
        //[JsonProperty("validationErrors")]
        public List<ModelValidationError> ValidationErrors { get; set; }
    }
}