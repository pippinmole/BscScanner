using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BscScanner.Data;
using Newtonsoft.Json;

namespace BscScanner;

public class BscScanClient(string apiKey) : IBscScanClient, IDisposable
{
    private readonly HttpClient _client = new();

    #region Account
        
    public async Task<float> GetBnbBalanceSingleAsync(string address) {
        var url =
            $"https://api.bscscan.com/api?module=account&action=balance&address={address}&tag=latest&apikey={apiKey}";
        var obj = await Get<BscBalanceSingleSchema>(_client, url);

        // Disgusting, but BscScan API is like this.
        return float.Parse(obj.Result);
    }
    public async Task<IEnumerable<BscBalance>> GetBnbBalanceMultipleAsync(IEnumerable<string> addresses) {
        var url = $"https://api.bscscan.com/api?module=account&action=balancemulti&address="
                  + string.Join(",", addresses)
                  + $"&tag=latest&apikey={apiKey}";
        var obj = await Get<BscBalanceMultipleSchema>(_client, url).ConfigureAwait(false);
        return obj.Balances;
    }
    public async Task<IEnumerable<BscTransaction>> GetTransactionsByAddress(string address, int startBlock = 1, int endBlock = 99999999) {
        var url = $"https://api.bscscan.com/api?module=account&action=txlist&address={address}&startblock={startBlock}&endblock={endBlock}&sort=asc&apikey={apiKey}";
        var obj = await Get<BscTransactionSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }
    public async Task<IEnumerable<BscTransaction>> GetTransactionsByHash(string hash) {
        var url = $"https://api.bscscan.com/api?module=account&action=txlistinternal&txhash={hash}&apikey={apiKey}";
        var obj = await Get<BscTransactionSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }
    public async Task<IEnumerable<BscTransaction>> GetTransactionsByBlockRange(int startBlock = 1, int endBlock = 99999999) {
        var url =
            $"https://api.bscscan.com/api?module=account&action=txlistinternal&startblock={startBlock}&endblock={endBlock}&sort=asc&apikey={apiKey}";
        var obj = await Get<BscTransactionSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }

    public async Task<IEnumerable<BscTransaction>> GetBep20TokenTransfersByAddress(string address = null, string contractAddress = null) {
        var str = new StringBuilder("https://api.bscscan.com/api?module=account&action=tokentx");
        if (address is not null)
            str.Append($"&address={address}");
            
        if (contractAddress is not null)
            str.Append($"&contractaddress={contractAddress}");

        var obj = await Get<BscTransactionSchema>(_client, str.Append($"&sort=asc&apikey={apiKey}").ToString()).ConfigureAwait(false);
        return obj.Result;
    }

    public Task<IEnumerable<BscTransaction>> GetBep20TokenTransferByContractAddress(string contract)
        => GetBep20TokenTransfersByAddress(contractAddress: contract);

    public Task<IEnumerable<BscTransaction>> GetErc721TokenTransferByContractAddress(string contract)
        => GetErc721TokenTransfersByAddress(contractAddress: contract);

    public async Task<IEnumerable<BscTransaction>> GetErc721TokenTransfersByAddress(string address = null, string contractAddress = null){
        var str = new StringBuilder("https://api.bscscan.com/api?module=account&action=tokennfttx");
        if (address is not null)
            str.Append($"&address={address}");

        if (contractAddress is not null)
            str.Append($"&contractaddress={contractAddress}");
        var obj = await Get<BscTransactionSchema>(_client, str.Append($"&sort=asc&apikey={apiKey}").ToString()).ConfigureAwait(false);
        return obj.Result;
    }
    public async Task<IEnumerable<BscBlock>> GetBlocksValidatedByAddress(string address) {
        var url =
            $"https://api.bscscan.com/api?module=account&action=getminedblocks&address={address}&sort=asc&apikey={apiKey}";
        var obj = await Get<BscBlockScheme>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }

    #endregion

    #region Contracts

    public async Task<string> GetAbiFromSourceAddress(string address) {
        var url =
            $"https://api.bscscan.com/api?module=contract&action=getabi&address={address}&apikey={apiKey}";
        var obj = await Get<BscAbiSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }  
        
    public async Task<IEnumerable<BscContract>> GetSourceCodeFromSourceAddress(string address) {
        var url =
            $"https://api.bscscan.com/api?module=contract&action=getsourcecode&address={address}&apikey={apiKey}";
        var obj = await Get<BscSourceCodeSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }
        
    #endregion

    #region Transactions

    public async Task<BscTxStatus> GetTransactionReceiptStatus(string txHash) {
        var url =
            $"https://api.bscscan.com/api?module=transaction&action=gettxreceiptstatus&txhash={txHash}&apikey={apiKey}";
        var obj = await Get<BscTxReceiptSchema>(_client, url).ConfigureAwait(false);
        return obj.Result.Result == "0" ? BscTxStatus.Fail : BscTxStatus.Pass;
    }

    #endregion

    #region Blocks

    public async Task<BscBlockReward> GetBlockRewardByBlock(int block) {
        var url =
            $"https://api.bscscan.com/api?module=block&action=getblockreward&blockno={block}&apikey={apiKey}";
        var obj = await Get<BscBlockRewardSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }
        
    public async Task<BscBlockCountdown> GetBlockCountdownByBlock(int block) {
        var url =
            $"https://api.bscscan.com/api?module=block&action=getblockcountdown&blockno={block}&apikey={apiKey}";
        var obj = await Get<BscBlockCountdownSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }

    public async Task<int> GetBlockNumberByTimestamp(DateTime time) {
        var unixTime = ((DateTimeOffset) time).ToUnixTimeSeconds();
        return await this.GetBlockNumberByTimestamp(unixTime);
    }

    public async Task<int> GetBlockNumberByTimestamp(long unixTime) {
        var url =
            $"https://api.bscscan.com/api?module=block&action=getblocknobytime&timestamp={unixTime}&closest=before&apikey={apiKey}";
        var obj = await Get<BscBlockByTimeSchema>(_client, url).ConfigureAwait(false);
        return int.Parse(obj.Result);
    }

    #endregion

    #region GEth/Proxy

    public async Task<int> GetLatestBlock() {
        var url =
            $"https://api.bscscan.com/api?module=proxy&action=eth_blockNumber&apikey={apiKey}";
        var obj = await Get<BscLatestBlockSchema>(_client, url).ConfigureAwait(false);

        return Convert.ToInt32(obj.Result, 16);
    }

    #endregion

    #region Tokens

    public async Task<double> GetTokenTotalSupply(string address) {
        var url =
            $"https://api.bscscan.com/api?module=stats&action=tokensupply&contractaddress={address}&apikey={apiKey}";
        var obj = await Get<BscTokenTotalSupplySchema>(_client, url).ConfigureAwait(false);
        return double.Parse(obj.Result);
    }
        
    public async Task<double> GetTokenCirculatingSupply(string address) {
        var url =
            $"https://api.bscscan.com/api?module=stats&action=tokenCsupply&contractaddress={address}&apikey={apiKey}";
        var obj = await Get<BscTokenCirculatingSupplySchema>(_client, url).ConfigureAwait(false);
        return double.Parse(obj.Result);
    }
        
    public async Task<double> GetAccountBalanceByContractAddress(string contractAddress, string accountAddress) {
        var url =
            $"https://api.bscscan.com/api?module=account&action=tokenbalance&contractaddress={contractAddress}&address={accountAddress}&tag=latest&apikey={apiKey}";
        var obj = await Get<BscTokenCirculatingSupplySchema>(_client, url).ConfigureAwait(false);
        return double.Parse(obj.Result);
    }

    #endregion

    #region Stats

    public async Task<double> GetBnbTotalSupply() {
        var url =
            $"https://api.bscscan.com/api?module=stats&action=bnbsupply&apikey={apiKey}";
        var obj = await Get<BscBnbTotalSupplySchema>(_client, url).ConfigureAwait(false);
        return double.Parse(obj.Result);
    }

    public async Task<IEnumerable<BscValidator>> GetBscValidators() {
        var url =
            $"https://api.bscscan.com/api?module=stats&action=validators&apikey={apiKey}";
        var obj = await Get<BscValidatorSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }
        
    public async Task<BscBnbPrice> GetBnbLastPrice() {
        var url =
            $"https://api.bscscan.com/api?module=stats&action=bnbprice&apikey={apiKey}";
        var obj = await Get<BscBnbLastPriceSchema>(_client, url).ConfigureAwait(false);
        return obj.Result;
    }
        
    #endregion
        
    private readonly JsonSerializerSettings _serializerSettings = new() {
        Error = (_, ev) => ev.ErrorContext.Handled = true
    };
        
    private async Task<T> Get<T>(HttpClient client, string url) {
        var json = await client.GetStringAsync(url);
        var obj = JsonConvert.DeserializeObject<T>(json, _serializerSettings);

        if (obj is not IBscResult { Status: "0" } result) 
            return obj;
        switch (result.Message)
        {
            case "NOTOK":
                var error = new BscError();
                JsonConvert.PopulateObject(json, error);
                switch(error.ErrorMessage)
                {
                    case "Max rate limit reached":
                        throw new HttpRequestException(error.ErrorMessage, null);
                    case "Error! Block number already pass":
                        //throw new HttpRequestException(error.ErrorMessage,null,HttpStatusCode.BadRequest);
                        break; //Ignore to keep same result
                    default:
                        throw new HttpRequestException(error.ErrorMessage, null);
                }
                break;
            case "No transactions found":
                //throw new HttpRequestException(result.Message, null, HttpStatusCode.NoContent);
                break; //Ignore to keep same result
            default:
                throw new HttpRequestException(result.Message);
        }

        return obj;
    }

    public void Dispose() {
        _client?.Dispose();
    }
}