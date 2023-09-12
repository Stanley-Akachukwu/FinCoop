using System.Text.Json.Serialization;

namespace AP.ChevronCoop.Commons
{
    [Serializable]
    //[JsonObject(MemberSerialization.OptIn)]
    //[JsonObject("validationerror")]
    //[JsonObject(IsReference = false)]
    public class ModelValidationError
    {

        public ModelValidationError()
        {

        }

        [JsonPropertyName("entity")]
        //[JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonPropertyName("field")]
        //[JsonProperty("field")]
        public string FieldName { get; set; }

        [JsonPropertyName("type")]
        //[JsonProperty("type")]
        public string FieldType { get; set; }

        [JsonPropertyName("value")]
        //[JsonProperty("value")]
        public object Value { get; set; }

        [JsonPropertyName("rawValue")]
        //[JsonProperty("value")]
        public object RawValue { get; set; }


        [JsonPropertyName("error")]
        //[JsonProperty("error")]
        public string Error { get; set; }

        [JsonPropertyName("code")]
        // [JsonProperty("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        //[JsonProperty("description")]
        public string Description { get; set; }




    }
}