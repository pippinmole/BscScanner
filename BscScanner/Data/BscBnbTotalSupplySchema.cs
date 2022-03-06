using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBnbTotalSupplySchema : BscResult {
        [JsonProperty("result")] public string Result { get; private set; }
    }
}