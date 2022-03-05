using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBlockByTimeSchema : BscResult {
        [JsonProperty("result")] public string Result { get; private set; }
    }
}