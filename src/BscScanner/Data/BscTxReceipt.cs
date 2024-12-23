using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscTxReceipt
{
    [JsonProperty("result")] public string Result { get; set; }
}