using Newtonsoft.Json;

namespace BscScanner.Data {
    public class BscBlockCountdown {
        [JsonProperty("CurrentBlock")] public string CurrentBlock { get; set; }
        [JsonProperty("CountdownBlock")] public string CountdownBlock { get; set; }
        [JsonProperty("RemainingBlock")] public string RemainingBlock { get; set; }
        [JsonProperty("EstimateTimeInSec")] public string EstimateTimeInSec { get; set; }
    }
}