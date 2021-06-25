[![NuGet version (BscScanner)](https://img.shields.io/github/license/pippinmole/bscscanner?style=for-the-badge)](https://www.nuget.org/packages/BscScanner)
[![NuGet downloads](https://img.shields.io/nuget/dt/BscScanner?style=for-the-badge)](https://www.nuget.org/packages/BscScanner)
[![Code size](https://img.shields.io/github/languages/code-size/pippinmole/bscscanner?style=for-the-badge)](https://github.com/pippinmole/BscScanner)

# BscScanner

BscScanner is a .NET wrapper for the [BscScan API](https://bscscan.com/apis).

## How to use

1. Install the [Nuget](https://www.nuget.org/packages/BscScanner) package
    ```cli
    Install-Package BscScanner
    ```
2. Instantiate the BscScanClient
    ```cs
    var client = new BscScanClient(apiKey);
    var balance = await client.GetBnbBalanceSingleAsync("0x59784ccC71205eF6A292F973e44f46CdC1f58306");
    ```

## Notes

The BscScan API uses Gwei units, which means you will need to convert them to BNB values for any front-end uses. I suggest [BscScan.Extensions.Convert](https://www.nuget.org/packages/BscScanner.Extensions.Convert/) for this:

```cs
var client = new BscScanClient(apiKey);
var balanceGwei = await client.GetBnbBalanceSingleAsync("address");
var balanceBnb = BscConvert.GweiToBnb(balanceGwei);
```

## How to contribute

You can contribute by pulling the code, making some changes and then opening a pull request. The changes will be revised and merged into main if it is a valid feature/bug fix.

1. Pull the latest main branch
2. Nuget restore to get dependencies
3. Make some changes
4. Create a pull request with a detailed explanation on why the changes were made

You can also donate BNB or Ether in order for me to maintain the project to this address: 0x59784ccC71205eF6A292F973e44f46CdC1f58306
