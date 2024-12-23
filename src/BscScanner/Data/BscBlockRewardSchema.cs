using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscBlockRewardSchema : BscResult {
        
    [JsonProperty("result")] public BscBlockReward Result { get; set; }
        
}