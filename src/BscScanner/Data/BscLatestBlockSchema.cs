using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscLatestBlockSchema : BscResult {
    [JsonProperty("jsonrpc")] public string JsonRpc { get; set; }
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("result")] public string Result { get; set; }
}