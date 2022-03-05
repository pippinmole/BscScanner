using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscSourceCodeSchema : BscResult {
        [JsonProperty("result")] public IEnumerable<BscContract> Result { get; private set; }
    }
}