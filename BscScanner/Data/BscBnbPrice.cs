using Newtonsoft.Json;

namespace BscScanner.Data {
    public class BscBnbPrice {
        [JsonProperty("ethbtc")] public string EthBtc { get; set; }
        [JsonProperty("ethbtc_timestamp")] public string EthBtcTimestamp { get; set; }
        [JsonProperty("ethusd")] public string EthUsd { get; set; }
        [JsonProperty("ethusd_timestamp")] public string EthUsdTimestamp { get; set; }
    }
}