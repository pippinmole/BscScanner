using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests {
    internal class BlocksApiTests {
        
        private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");

        [Test]
        public async Task RunGetBlockRewardByBlock() {
            var blockReward = await BscScanClient.GetBlockRewardByBlock(2150000);

            Assert.AreEqual(blockReward.BlockNumber, "2150000");
            Assert.AreEqual(blockReward.TimeStamp, "1605122780");
        }
        
        [Test]
        public async Task RunGetBlockCountdownByBlockPassed() {
            var blockReward = await BscScanClient.GetBlockCountdownByBlock(8000000);

            Assert.AreEqual(blockReward, null);
        }
        
        [Test]
        public async Task RunGetBlockCountdownByBlockUpcoming() {
            var blockReward = await BscScanClient.GetBlockCountdownByBlock(80000000);

            Assert.AreNotEqual(blockReward, null);
        }

        [Test]
        public async Task Get() {
            var result = await BscScanClient.GetBlockRewardByBlock(8502906);
        }
        
    }
}