using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BscScanner.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BscScanner {
    public class BscScanClient : IBscScanClient, IDisposable {

        private readonly string _apiKey;
        private readonly HttpClient _client = new();

        public BscScanClient(string apiKey) {
            _apiKey = apiKey;
        }

        #region Account 
        
        public async Task<float> GetBnbBalanceSingleAsync(string address) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=balance&address={address}&tag=latest&apikey={_apiKey}";
            var obj = await Get<BscBalanceSingleSchema>(_client, url);

            // Disgusting, but BscScan API is like this.
            return float.Parse(obj.Result);
        }
        public async Task<IEnumerable<BscBalance>> GetBnbBalanceMultipleAsync(IEnumerable<string> addresses) {
            var url = $"https://api.bscscan.com/api?module=account&action=balancemulti&address="
                      + string.Join(",", addresses)
                      + $"&tag=latest&apikey={_apiKey}";
            var obj = await Get<BscBalanceMultipleSchema>(_client, url).ConfigureAwait(false);
            return obj.Balances;
        }
        public async Task<IEnumerable<BscTransaction>> GetTransactionsByAddress(string address, int startBlock = 1, int endBlock = 99999999) {
            var url = $"https://api.bscscan.com/api?module=account&action=txlist&address={address}&startblock={startBlock}&endblock={endBlock}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BscTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscTransaction>> GetTransactionsByHash(string hash) {
            var url = $"https://api.bscscan.com/api?module=account&action=txlistinternal&txhash={hash}&apikey={_apiKey}";
            var obj = await Get<BscTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscTransaction>> GetTransactionsByBlockRange(int startBlock = 1, int endBlock = 99999999) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=txlistinternal&startblock={startBlock}&endblock={endBlock}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BscTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }

        public async Task<IEnumerable<BscTransaction>> GetBep20TokenTransfersByAddress(string address = null, string contractAddress = null) {
            var str = new StringBuilder("https://api.bscscan.com/api?module=account&action=tokentx");
            if (address is not null)
                str.Append($"&address={address}");
            
            if (contractAddress is not null)
                str.Append($"&contractaddress={contractAddress}");

            var obj = await Get<BscTransactionSchema>(_client, str.Append($"&sort=asc&apikey={_apiKey}").ToString()).ConfigureAwait(false);
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
            var obj = await Get<BscTransactionSchema>(_client, str.Append($"&sort=asc&apikey={_apiKey}").ToString()).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscBlock>> GetBlocksValidatedByAddress(string address) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=getminedblocks&address={address}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BscBlockScheme>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }

        #endregion

        #region Contracts

        public async Task<string> GetAbiFromSourceAddress(string address) {
            var url =
                $"https://api.bscscan.com/api?module=contract&action=getabi&address={address}&apikey={_apiKey}";
            var obj = await Get<BscAbiSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }  
        
        public async Task<IEnumerable<BscContract>> GetSourceCodeFromSourceAddress(string address) {
            var url =
                $"https://api.bscscan.com/api?module=contract&action=getsourcecode&address={address}&apikey={_apiKey}";
            var obj = await Get<BscSourceCodeSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        
        #endregion

        #region Transactions

        public async Task<TxStatus> GetTransactionReceiptStatus(string txHash) {
            var url =
                $"https://api.bscscan.com/api?module=transaction&action=gettxreceiptstatus&txhash={txHash}&apikey={_apiKey}";
            var obj = await Get<BscTxReceiptSchema>(_client, url).ConfigureAwait(false);
            return obj.Result.Result == "0" ? TxStatus.Fail : TxStatus.Pass;
        }

        #endregion

        #region Blocks

        public async Task<BscBlockReward> GetBlockRewardByBlock(int block) {
            var url =
                $"https://api.bscscan.com/api?module=block&action=getblockreward&blockno={block}&apikey={_apiKey}";
            var obj = await Get<BscBlockRewardSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        
        public async Task<BscBlockCountdown> GetBlockCountdownByBlock(int block) {
            var url =
                $"https://api.bscscan.com/api?module=block&action=getblockcountdown&blockno={block}&apikey={_apiKey}";
            var obj = await Get<BscBlockCountdownSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }

        public async Task<int> GetBlockNumberByTimestamp(DateTime time) {
            var unixTime = ((DateTimeOffset) time).ToUnixTimeSeconds();
            return await this.GetBlockNumberByTimestamp(unixTime);
        }

        public async Task<int> GetBlockNumberByTimestamp(long unixTime) {
            var url =
                $"https://api.bscscan.com/api?module=block&action=getblocknobytime&timestamp={unixTime}&closest=before&apikey={_apiKey}";
            var obj = await Get<BscBlockByTime>(_client, url).ConfigureAwait(false);
            return int.Parse(obj.Result);
        }

        #endregion

        #region GEth/Proxy

        public async Task<int> GetLatestBlock() {
            var url =
                $"https://api.bscscan.com/api?module=proxy&action=eth_blockNumber&apikey={_apiKey}";
            var obj = await Get<BscLatestBlock>(_client, url).ConfigureAwait(false);

            return Convert.ToInt32(obj.Result, 16);
        }

        #endregion

        #region Tokens

        public async Task<double> GetTokenTotalSupply(string address) {
            var url =
                $"https://api.bscscan.com/api?module=stats&action=tokensupply&contractaddress={address}&apikey={_apiKey}";
            var obj = await Get<BscTokenTotalSupplySchema>(_client, url).ConfigureAwait(false);
            return double.Parse(obj.Result);
        }
        
        public async Task<double> GetTokenCirculatingSupply(string address) {
            var url =
                $"https://api.bscscan.com/api?module=stats&action=tokenCsupply&contractaddress={address}&apikey={_apiKey}";
            var obj = await Get<BscTokenCirculatingSupplySchema>(_client, url).ConfigureAwait(false);
            return double.Parse(obj.Result);
        }
        
        public async Task<double> GetAccountBalanceByContractAddress(string contractAddress, string accountAddress) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=tokenbalance&contractaddress={contractAddress}&address={accountAddress}&tag=latest&apikey={_apiKey}";
            var obj = await Get<BscTokenCirculatingSupplySchema>(_client, url).ConfigureAwait(false);
            return double.Parse(obj.Result);
        }

        #endregion

        #region Stats

        public async Task<double> GetBnbTotalSupply() {
            var url =
                $"https://api.bscscan.com/api?module=stats&action=bnbsupply&apikey={_apiKey}";
            var obj = await Get<BscBnbTotalSupply>(_client, url).ConfigureAwait(false);
            return double.Parse(obj.Result);
        }

        public async Task<IEnumerable<BscValidator>> GetBscValidators() {
            var url =
                $"https://api.bscscan.com/api?module=stats&action=validators&apikey={_apiKey}";
            var obj = await Get<BscValidatorSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        
        public async Task<BscBnbPrice> GetBnbLastPrice() {
            var url =
                $"https://api.bscscan.com/api?module=stats&action=bnbprice&apikey={_apiKey}";
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
            
            return obj;
        }

        public void Dispose() {
            _client?.Dispose();
        }
    }
}