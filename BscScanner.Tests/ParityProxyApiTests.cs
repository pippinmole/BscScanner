using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests {
    internal class ParityProxyApiTests {
        
        private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");
        
        [Test]
        public async Task RunGetLatestBlock() {
            Assert.DoesNotThrowAsync(async () => await BscScanClient.GetLatestBlock());
        }
    }
}