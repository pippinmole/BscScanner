using Newtonsoft.Json;

namespace BscScanner.Data;

internal abstract class BscResult : IBscResult
{
    [JsonProperty("status")] public string Status { get; private set; }
    [JsonProperty("message")] public string Message { get; private set; }
}