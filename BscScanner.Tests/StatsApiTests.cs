using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests {
    internal class StatsApiTests {
        
        private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");
        
        [Test]
        public async Task RunGetBnbTotalSupply() {
            var amount = await BscScanClient.GetBnbTotalSupply();
            
            Assert.NotZero(amount); 
        }
        
        [Test]
        public async Task RunGetBscValidators() {
            var amount = await BscScanClient.GetBscValidators();
            
            Assert.NotZero(amount.Count()); 
        }
        
        [Test]
        public async Task RunGetBnbLastPrice() {
            var amount = await BscScanClient.GetBnbLastPrice();
            
            Assert.IsNotEmpty(amount.EthBtc);
            Assert.IsNotEmpty(amount.EthUsd);
            Assert.IsNotEmpty(amount.EthBtcTimestamp);
            Assert.IsNotEmpty(amount.EthUsdTimestamp);
        }
    }
}