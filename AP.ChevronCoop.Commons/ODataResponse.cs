using System.Text.Json.Serialization;

namespace AP.ChevronCoop.Commons
{
    //[JsonObject(IsReference = false)]
    public class ODataResponse<T>
    {

        [JsonPropertyName("@odata.context")]
        //[JsonProperty("@odata.context")]
        public string Context { get; set; }


        [JsonPropertyName("@odata.count")]
        //[JsonProperty("@odata.count")]
        public int TotalRows { get; set; }


        [JsonPropertyName("value")]
        //[JsonProperty("value", IsReference = false)]
        public List<T> Data { get; set; }

    }
}