using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var obj = await Get<BnbBalanceSingleSchema>(_client, url);

            // Disgusting, but BscScan API is like this.
            return float.Parse(obj.Result);
        }
        public async Task<IEnumerable<BnbBalance>> GetBnbBalanceMultipleAsync(IEnumerable<string> addresses) {
            var url = $"https://api.bscscan.com/api?module=account&action=balancemulti&address="
                      + string.Join(",", addresses)
                      + $"&tag=latest&apikey={_apiKey}";
            var obj = await Get<BnbBalanceMultipleSchema>(_client, url).ConfigureAwait(false);
            return obj.Balances;
        }
        public async Task<IEnumerable<BscTransaction>> GetTransactionsByAddress(string address, int startBlock = 1, int endBlock = 99999999) {
            var url = $"https://api.bscscan.com/api?module=account&action=txlist&address={address}&startblock={startBlock}&endblock={endBlock}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BnbTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscTransaction>> GetTransactionsByHash(string hash) {
            var url = $"https://api.bscscan.com/api?module=account&action=txlistinternal&txhash={hash}&apikey={_apiKey}";
            var obj = await Get<BnbTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscTransaction>> GetTransactionsByBlockRange(int startBlock = 1, int endBlock = 99999999) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=txlistinternal&startblock={startBlock}&endblock={endBlock}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BnbTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscTransaction>> GetBep20TokenTransfersByAddress(string address) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=tokentx&address={address}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BnbTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscTransaction>> GetErc721TokenTransfersByAddress(string address) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=tokennfttx&address={address}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BnbTransactionSchema>(_client, url).ConfigureAwait(false);
            return obj.Result;
        }
        public async Task<IEnumerable<BscBlock>> GetBlocksValidatedByAddress(string address) {
            var url =
                $"https://api.bscscan.com/api?module=account&action=getminedblocks&address={address}&sort=asc&apikey={_apiKey}";
            var obj = await Get<BnbBlockScheme>(_client, url).ConfigureAwait(false);
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

        #endregion

        private readonly JsonSerializerSettings _serializerSettings = new() {
            Error = (_, ev) => ev.ErrorContext.Handled = true
        };
        
        private async Task<T> Get<T>(HttpClient client, string url) {
            var json = await client.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<T>(json, _serializerSettings);
            
            Console.WriteLine(json);
            
            return obj;
        }

        public void Dispose() {
            _client?.Dispose();
        }
    }
}