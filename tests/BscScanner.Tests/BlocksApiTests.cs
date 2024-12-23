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

        Assert.That(blockReward.BlockNumber, Is.EqualTo("2150000"));
        Assert.That(blockReward.TimeStamp, Is.EqualTo("1605122780"));
    }

    [Test]
    public async Task RunGetBlockCountdownByBlockPassed()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockCountdownByBlock(8000000)
        );

        Assert.That(blockReward, Is.Null);
    }

    [Test]
    public async Task RunGetBlockCountdownByBlockUpcoming()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockCountdownByBlock(6022687)
        );
        
        Assert.That(blockReward, Is.Null);
    }

    [Test]
    public async Task RunGetBlockNumberByTimestampLong()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockNumberByTimestamp(1624569213)
        );

        Assert.That(blockReward, Is.EqualTo(8586272));
    }

    [Test]
    public async Task RunGetBlockNumberByTimestampDateTime()
    {
        await Helpers.AvoidRateLimitAsync();

        var blockReward = await RetryPolicy.BscScan.ExecuteAsync(() =>
            BscScanClient.GetBlockNumberByTimestamp(new DateTime(2021, 06, 24, 9 + 12, 13, 33, DateTimeKind.Utc))
        );

        Assert.That(blockReward, Is.EqualTo(8586272));
    }
}