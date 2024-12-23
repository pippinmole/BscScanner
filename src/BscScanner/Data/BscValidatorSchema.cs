using System.Collections.Generic;
using Newtonsoft.Json;

namespace BscScanner.Data;

internal class BscValidatorSchema : BscResult {
    [JsonProperty("result")] public IEnumerable<BscValidator> Result { get; set; }
}