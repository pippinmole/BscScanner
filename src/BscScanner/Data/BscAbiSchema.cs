using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscAbiSchema : BscResult {
       
    [JsonProperty("result")] public string Result { get; private set; }
}