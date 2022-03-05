using System.Threading.Tasks;
using BscScanner.Data;
using NUnit.Framework;

namespace BscScanner.Tests {
    internal class TransactionApiTests {
        
        private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");

        [Test]
        public async Task RunGetTransactionReceiptStatus() {
            var balance = await RetryPolicy.BscScan.ExecuteAsync(() => 
                BscScanClient.GetTransactionReceiptStatus("0xe9975702518c79caf81d5da65dea689dcac701fcdd063f848d4f03c85392fd00")
            );
            Assert.AreEqual(balance, BscTxStatus.Pass);
        }
        
    }
}