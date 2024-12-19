using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscError : BscResult
{
    [JsonProperty("result")] public string ErrorMessage { get; private set; }
}