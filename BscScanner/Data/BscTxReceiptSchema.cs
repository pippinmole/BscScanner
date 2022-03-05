using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscTxReceiptSchema : BscResult {
        [JsonProperty("result")] public BscTxReceipt Result { get; set; }
    }
}