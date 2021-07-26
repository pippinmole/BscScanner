using System;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace BscScanner.Tests {
    internal class GenericApiTests {

        [Test]
        public void TestBscScanClientDispose() {

            var client = new BscScanClient("key");
            
            client.Dispose();

            Assert.CatchAsync<ObjectDisposedException>(async () => {
                await client.GetBscValidators();
            });
        }
        
    }
}