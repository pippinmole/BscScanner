using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBlockCountdownSchema {
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("result")] public BscBlockCountdown Result { get; set; }
    }
}