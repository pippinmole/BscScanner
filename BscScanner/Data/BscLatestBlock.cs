using Newtonsoft.Json;

namespace BscScanner.Data {
    public class BscLatestBlock {
        [JsonProperty("jsonrpc")] public string JsonRpc { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("result")] public string Result { get; set; }
    }
}