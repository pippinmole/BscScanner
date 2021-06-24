using Newtonsoft.Json;

namespace BscScanner.Data {
    internal class BscTxReceiptSchema {
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("result")] public TxReceipt Result { get; set; }
    }

    internal class TxReceipt {
        [JsonProperty("result")] public string Result { get; set; }
    }
    
    public enum TxStatus {
        Fail = 0,
        Pass = 1
    }
}