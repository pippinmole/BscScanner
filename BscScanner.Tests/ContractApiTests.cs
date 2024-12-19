using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests;

internal sealed class ContractApiTests {
        
    private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");

    [Test]
    public async Task RunGetAbiFromSource() {
        var balance = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetAbiFromSourceAddress("0x0000000000000000000000000000000000001004")
        );

        Assert.IsNotEmpty(balance);
    }
        
    [Test]
    public async Task RunGetSourceCodeFromSource() {
        var balance = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetSourceCodeFromSourceAddress("0x0000000000000000000000000000000000001004")
        );

        Assert.IsNotEmpty(balance);
    }
}