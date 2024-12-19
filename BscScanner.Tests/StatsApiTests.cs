using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests;

internal sealed class StatsApiTests {
        
    private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");
        
    [Test]
    public async Task RunGetBnbTotalSupply() {
        await Helpers.AvoidRateLimitAsync();

        var amount = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetBnbTotalSupply()
        );
            
        Assert.That(amount, Is.Not.Zero);
    }
        
    [Test]
    public async Task RunGetBscValidators() {
        await Helpers.AvoidRateLimitAsync();

        var amount = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetBscValidators()
        );
            
        Assert.That(amount, Is.Not.Empty);
    }
        
    [Test]
    public async Task RunGetBnbLastPrice() {
        await Helpers.AvoidRateLimitAsync();

        var amount = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetBnbLastPrice()
        );
            
        Assert.That(amount.EthBtc, Is.Not.Empty);
        Assert.That(amount.EthUsd, Is.Not.Empty);
        Assert.That(amount.EthBtcTimestamp, Is.Not.Empty);
        Assert.That(amount.EthUsdTimestamp, Is.Not.Empty);
    }
}