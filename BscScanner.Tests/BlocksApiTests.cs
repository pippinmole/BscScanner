using System;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests;

internal sealed class BlocksApiTests
{

    private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");

    [Test]
    public async Task RunGetBlockRewardByBlock()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockRewardByBlock(2150000)
        );

        Assert.AreEqual("2150000", blockReward.BlockNumber);
        Assert.AreEqual("1605122780", blockReward.TimeStamp);
    }

    [Test]
    public async Task RunGetBlockCountdownByBlockPassed()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockCountdownByBlock(8000000)
        );

        Assert.AreEqual(null, blockReward);
    }

    [Test]
    public async Task RunGetBlockCountdownByBlockUpcoming()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockCountdownByBlock(6022687)
        );
        
        Assert.IsNull(blockReward);
    }

    [Test]
    public async Task RunGetBlockNumberByTimestampLong()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockNumberByTimestamp(1624569213)
        );

        Assert.AreEqual(8586272, blockReward);
    }

    [Test]
    public async Task RunGetBlockNumberByTimestampDateTime()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockNumberByTimestamp(new DateTime(2021, 06, 24, 9 + 12, 13, 33, DateTimeKind.Utc))
        );

        Assert.AreEqual(8586272, blockReward);
    }
}