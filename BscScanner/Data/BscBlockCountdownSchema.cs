using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscBlockCountdownSchema : BscResult {
    [JsonProperty("result")] public BscBlockCountdown Result { get; set; }
}