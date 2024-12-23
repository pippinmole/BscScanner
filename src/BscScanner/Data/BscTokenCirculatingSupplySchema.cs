using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscTokenCirculatingSupplySchema : BscResult {
    [JsonProperty("result")] public string Result { get; private set; }
}