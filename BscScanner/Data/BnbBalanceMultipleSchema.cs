using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BnbBalanceMultipleSchema {
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("result")] public List<BnbBalance> Balances { get; set; }
    }

    public class BnbBalance {
        [JsonProperty("account")] public string Account { get; set; }
        [JsonProperty("balance")] public string Balance { get; set; }
    }
}