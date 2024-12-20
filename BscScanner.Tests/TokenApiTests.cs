﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BscScanner.Tests;

internal sealed class TokenApiTests {
        
    private static readonly IBscScanClient BscScanClient = new BscScanClient("7SYTNQ2B5SS7GR4WATVTFXP52BWSUK5PUJ");
        
    [Test]
    public async Task RunGetTokenTotalSupply() {
        await Helpers.AvoidRateLimitAsync();

        var supply = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetTokenTotalSupply("0xe9e7cea3dedca5984780bafc599bd69add087d56")
        );
            
        Console.WriteLine(supply.ToString("F2"));
        
        Assert.That(supply, Is.EqualTo(365596245875680231573946368.00));
    }
        
    [Test]
    public async Task RunGetTokenCirculatingSupply() {
        await Helpers.AvoidRateLimitAsync();

        Assert.DoesNotThrowAsync(async () => await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetTokenCirculatingSupply("0xe9e7cea3dedca5984780bafc599bd69add087d56"))
        );
    }
        
    [Test]
    public async Task RunGetAccountBalanceByContractAddressNull() {
        await Helpers.AvoidRateLimitAsync();

        Assert.CatchAsync(async () => await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetAccountBalanceByContractAddress("notatoken", "notanaddress"))
        );
    }
        
    [Test]
    public async Task RunGetAccountBalanceByContractAddressValid() {
        await Helpers.AvoidRateLimitAsync();

        var amount = await RetryPolicy.BscScan.ExecuteAsync(() => 
            BscScanClient.GetAccountBalanceByContractAddress("0xc6cb12df4520b7bf83f64c79c585b8462e18b6aa", "0x59784ccC71205eF6A292F973e44f46CdC1f58306")
        );
            
        Assert.That(amount, Is.Not.Zero);
    }
}