using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscBnbLastPriceSchema : BscResult {
        [JsonProperty("result")] public BscBnbPrice Result { get; private set; }
    }
}