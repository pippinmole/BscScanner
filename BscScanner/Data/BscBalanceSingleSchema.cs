using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscBalanceSingleSchema : BscResult {       
    [JsonProperty("result")] public string Result { get; private set; }
}