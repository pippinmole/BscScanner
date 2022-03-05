using Newtonsoft.Json;

namespace BscScanner.Data
{
    public class BscBalance {
        [JsonProperty("account")] public string Account { get; set; }
        [JsonProperty("balance")] public string Balance { get; set; }
    }
}
