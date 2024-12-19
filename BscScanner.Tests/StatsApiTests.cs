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
            
        Assert.NotZero(amount); 
    }
        
    [Test]
    public async Task RunGetBscValidators() {
        await Helpers.AvoidRateLimitAsync();

        var amount = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetBscValidators()
        );
            
        Assert.NotZero(amount.Count()); 
    }
        
    [Test]
    public async Task RunGetBnbLastPrice() {
        await Helpers.AvoidRateLimitAsync();

        var amount = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetBnbLastPrice()
        );
            
        Assert.IsNotEmpty(amount.EthBtc);
        Assert.IsNotEmpty(amount.EthUsd);
        Assert.IsNotEmpty(amount.EthBtcTimestamp);
        Assert.IsNotEmpty(amount.EthUsdTimestamp);
    }
}