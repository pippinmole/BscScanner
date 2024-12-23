<div align="center">
   <img src="bscscanner.png" alt="BscScanner Logo">
   
   [![NuGet version (BscScanner)](https://img.shields.io/github/license/pippinmole/bscscanner?style=for-the-badge)](https://www.nuget.org/packages/BscScanner)
   [![NuGet downloads](https://img.shields.io/nuget/dt/BscScanner?style=for-the-badge)](https://www.nuget.org/packages/BscScanner)
   [![Code size](https://img.shields.io/github/languages/code-size/pippinmole/bscscanner?style=for-the-badge)](https://github.com/pippinmole/BscScanner)
   ![GitHub](https://img.shields.io/github/license/pippinmole/bscscanner?style=for-the-badge)
</div>

## BscScanner

BscScanner is a .NET wrapper for the [BscScan API](https://bscscan.com/apis).

### How to use

1. Install the [Nuget](https://www.nuget.org/packages/BscScanner) package
    ```cli
    Install-Package BscScanner
    ```
2. Instantiate the BscScanClient
    ```cs
    var client = new BscScanClient(apiKey);
    var balance = await client.GetBnbBalanceSingleAsync("0x59784ccC71205eF6A292F973e44f46CdC1f58306");
    ```

   > **NOTE**: The BscScan API uses Gwei units, which means you may need to convert them to BNB. [Here's a library for that](https://www.nuget.org/packages/BscScanner.Extensions.Convert/).

## BscScanner.Extensions.Convert
A conversion library to convert between Binance (Smart) Chain units

[![NuGet version (BscScanner)](https://img.shields.io/github/license/pippinmole/BscScanner.Extensions.Convert?style=for-the-badge)](https://www.nuget.org/packages/BscScanner.Extensions.Convert)
[![NuGet downloads](https://img.shields.io/nuget/dt/BscScanner.Extensions.Convert?style=for-the-badge)](https://www.nuget.org/packages/BscScanner.Extensions.Convert)
[![Code size](https://img.shields.io/github/languages/code-size/pippinmole/BscScanner.Extensions.Convert?style=for-the-badge)](https://github.com/pippinmole/BscScanner.Extensions.Convert)

### How to use

1. Install the [Nuget](https://www.nuget.org/packages/BscScanner.Extensions.Convert) package
    ```cli
    Install-Package BscScanner.Extensions.Convert
    ```
2. Convert units
    ```cs
    var gwei = 1254000000000000000;
    var bnb = BscConvert.GweiToBnb(gwei); // 1.254
    ```

## BscScanner.Extensions.DependencyInjection
BscScanner extensions for Microsoft.Extensions.DependencyInjection

[![NuGet version (BscScanner)](https://img.shields.io/github/license/pippinmole/BscScanner.Extensions.DependencyInjection?style=for-the-badge)](https://www.nuget.org/packages/BscScanner.Extensions.DependencyInjection)
[![NuGet downloads](https://img.shields.io/nuget/dt/BscScanner.Extensions.DependencyInjection?style=for-the-badge)](https://www.nuget.org/packages/BscScanner.Extensions.DependencyInjection)
[![Code size](https://img.shields.io/github/languages/code-size/pippinmole/BscScanner.Extensions.DependencyInjection?style=for-the-badge)](https://github.com/pippinmole/BscScanner.Extensions.DependencyInjection)

### How to use

1. Install the [Nuget](https://www.nuget.org/packages/BscScanner.Extensions.DependencyInjection) package
    ```cli
    Install-Package BscScanner.Extensions.DependencyInjection
    ```
2. Set up the service
    ```cs
    services.AddBscScanner(opt => {
        opt.ApiKey = "api key here";
    });
    ```
3. Inject the service into your classes (ASP.NET Core API example)
   ```cs
   [ApiController]
   [Route("[controller]")]
   public class BscScannerController : ControllerBase {

        private readonly IBscScanClient _bscScanClient;

        public BscScannerController(IBscScanClient bscScanClient) {
            _bscScanClient = bscScanClient;
        }

        [HttpGet]
        public async Task<float> GetBalanceFor(string address) {
            var result = await _bscScanClient.GetBnbBalanceSingleAsync(address);

            return result;
        }
   }
   ```

## How to contribute

You can contribute by pulling the code, making some changes and then opening a pull request. The changes will be revised and merged into main if it is a valid feature/bug fix.

1. Pull the latest main branch
2. Nuget restore to get dependencies
3. Make some changes
4. Create a pull request with a detailed explanation on why the changes were made

You can also donate BNB or Ether in order for me to maintain the project to this address: 0x59784ccC71205eF6A292F973e44f46CdC1f58306
