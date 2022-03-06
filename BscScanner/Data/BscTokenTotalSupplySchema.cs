using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscTokenTotalSupplySchema : BscResult {
        [JsonProperty("result")] public string Result { get; private set; }
    }
}