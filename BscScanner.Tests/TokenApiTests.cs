using System;
using System.Numerics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests {
    internal class TokenApiTests {
        
        private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");
        
        [Test]
        public async Task RunGetTokenTotalSupply() {
            var supply = await BscScanClient.GetTokenTotalSupply("0xe9e7cea3dedca5984780bafc599bd69add087d56");
            
            Assert.AreEqual(double.Parse("4200999999996203280118545563"), supply);
        }
        
        [Test]
        public async Task RunGetTokenCirculatingSupply() {
            Assert.DoesNotThrowAsync(async () => await BscScanClient.GetTokenCirculatingSupply("0xe9e7cea3dedca5984780bafc599bd69add087d56"));
        }
        
        [Test]
        public async Task RunGetAccountBalanceByContractAddressNull() {
            Assert.CatchAsync(async () =>
                await BscScanClient.GetAccountBalanceByContractAddress("notatoken", "notanaddress"));
        }
        
        [Test]
        public async Task RunGetAccountBalanceByContractAddressValid() {
            var amount = await BscScanClient.GetAccountBalanceByContractAddress("0xc6cb12df4520b7bf83f64c79c585b8462e18b6aa", "0x59784ccC71205eF6A292F973e44f46CdC1f58306");
            
            Assert.NotZero(amount);
        }
    }
}