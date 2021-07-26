using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests {
    internal class BlocksApiTests {
        
        private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");

        [Test]
        public async Task RunGetBlockRewardByBlock() {
            var blockReward = await BscScanClient.GetBlockRewardByBlock(2150000);

            Assert.AreEqual("2150000",blockReward.BlockNumber);
            Assert.AreEqual("1605122780", blockReward.TimeStamp);
        }
        
        [Test]
        public async Task RunGetBlockCountdownByBlockPassed() {
            var blockReward = await BscScanClient.GetBlockCountdownByBlock(8000000);

            Assert.AreEqual(null, blockReward);
        }
        
        [Test]
        public async Task RunGetBlockCountdownByBlockUpcoming() {
            var blockReward = await BscScanClient.GetBlockCountdownByBlock(80000000);

            Assert.AreNotEqual(null, blockReward);
        }

        [Test]
        public async Task RunGetBlockNumberByTimestampLong() {
            var blockReward = await BscScanClient.GetBlockNumberByTimestamp(1624569213);

            Assert.AreEqual(8586272, blockReward);
        }
        
        [Test]
        public async Task RunGetBlockNumberByTimestampDateTime() {
            var blockReward =
                await BscScanClient.GetBlockNumberByTimestamp(new DateTime(2021, 06, 24, 9+12, 13, 33, DateTimeKind.Utc));

            Assert.AreEqual(8586272, blockReward);
        }
    }
}