using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBlockByTime {
        [JsonProperty("status")] public string Status { get; private set; }
        [JsonProperty("message")] public string Message { get; private set; }
        [JsonProperty("result")] public string Result { get; private set; }
    }
}