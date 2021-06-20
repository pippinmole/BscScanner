using Newtonsoft.Json;

namespace BscScanner.Data {
    public class BscBlock {
        [JsonProperty("blockNumber")] public string BlockNumber { get; set; }
        [JsonProperty("timeStamp")] public string TimeStamp { get; set; }
        [JsonProperty("blockReward")] public string BlockReward { get; set; }
    }
}