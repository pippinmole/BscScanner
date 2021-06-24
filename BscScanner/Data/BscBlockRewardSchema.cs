using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBlockRewardSchema {
        
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("result")] public BscBlockReward Result { get; set; }
        
    }
}