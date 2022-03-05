using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBlockScheme : BscResult {
        [JsonProperty("result")] public IEnumerable<BscBlock> Result { get; private set; }
    }
}