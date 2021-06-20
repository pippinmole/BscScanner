using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BnbTransactionSchema {
        [JsonProperty("status")] public string Status { get; private set; }
        [JsonProperty("message")] public string Message { get; private set; }
        [JsonProperty("result")] public List<BscTransaction> Result { get; private set; }
    }
}