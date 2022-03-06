using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBalanceMultipleSchema : BscResult {
       
        [JsonProperty("result")] public List<BscBalance> Balances { get; set; }
    }
}