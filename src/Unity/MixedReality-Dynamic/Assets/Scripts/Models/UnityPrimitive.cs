using Newtonsoft.Json;

namespace MixedRealityData.Models
{
    public class UnityPrimitive
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("vector")]
        public Vector3 Vector { get; set; }
    }
}
