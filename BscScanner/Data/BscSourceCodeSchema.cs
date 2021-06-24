using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    public class BscSourceCodeSchema {
        [JsonProperty("status")] public string Status { get; private set; }
        [JsonProperty("message")] public string Message { get; private set; }
        [JsonProperty("result")] public IEnumerable<BscContract> Result { get; private set; }
    }
}