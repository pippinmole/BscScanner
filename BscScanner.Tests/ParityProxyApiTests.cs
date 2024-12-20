﻿using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests;

internal sealed class ParityProxyApiTests {
        
    private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");
        
    [Test]
    public async Task RunGetLatestBlock() {
        await Helpers.AvoidRateLimitAsync();

        Assert.DoesNotThrowAsync(async () => await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetLatestBlock())
        );
    }
}