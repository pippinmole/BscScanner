using System;
using NUnit.Framework;

namespace BscScanner.Tests;

internal sealed class GenericApiTests {

    [Test]
    public void TestBscScanClientDispose() {

        var client = new BscScanClient("key");
            
        client.Dispose();

        Assert.CatchAsync<ObjectDisposedException>(async () => {
            await client.GetBscValidators();
        });
    }
        
}