using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscTransactionSchema : BscResult {
        
        [JsonProperty("result")] public List<BscTransaction> Result { get; private set; }
    }
}