using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    public class BscBlockReward {
        [JsonProperty("blockNumber")] public string BlockNumber { get; set; }
        [JsonProperty("timeStamp")] public string TimeStamp { get; set; }
        [JsonProperty("blockMiner")] public string BlockMiner { get; set; }
        [JsonProperty("blockReward")] public string BlockReward { get; set; }
        [JsonProperty("uncles")] public List<BscUncle> Uncles { get; set; }
        [JsonProperty("uncleInclusionReward")] public string UncleInclusionReward { get; set; }
    }
}