using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BscScanner.Data;

namespace BscScanner {
    public interface IBscScanClient {
        
        #region Account
        
        Task<float> GetBnbBalanceSingleAsync(string address);
        Task<IEnumerable<BscBalance>> GetBnbBalanceMultipleAsync(IEnumerable<string> addresses);
        Task<IEnumerable<BscTransaction>> GetTransactionsByAddress(string address, int startBlock = 1,
            int endBlock = 99999999);
        Task<IEnumerable<BscTransaction>> GetTransactionsByHash(string hash);
        Task<IEnumerable<BscTransaction>> GetTransactionsByBlockRange(int startBlock = 1, int endBlock = 99999999);
        Task<IEnumerable<BscTransaction>> GetBep20TokenTransferByContractAddress(string contract);
        Task<IEnumerable<BscTransaction>> GetBep20TokenTransfersByAddress(string address = null, string contractAddress = null);
        Task<IEnumerable<BscTransaction>> GetErc721TokenTransfersByAddress(string address = null, string contractAddress = null);
        Task<IEnumerable<BscTransaction>> GetErc721TokenTransferByContractAddress(string contract);
        Task<IEnumerable<BscBlock>> GetBlocksValidatedByAddress(string address);
        
        #endregion

        #region Contracts

        Task<string> GetAbiFromSourceAddress(string address);
        Task<IEnumerable<BscContract>> GetSourceCodeFromSourceAddress(string address);
        
        #endregion

        #region Transaction

        Task<BscTxStatus> GetTransactionReceiptStatus(string txHash);
        
        #endregion

        #region Blocks
        
        Task<BscBlockReward> GetBlockRewardByBlock(int block);
        Task<BscBlockCountdown> GetBlockCountdownByBlock(int block);
        Task<int> GetBlockNumberByTimestamp(DateTime time);
        Task<int> GetBlockNumberByTimestamp(long unixTime);
        Task<int> GetLatestBlock();

        #endregion

        #region Token

        Task<double> GetTokenTotalSupply(string address);
        Task<double> GetTokenCirculatingSupply(string address);
        Task<double> GetAccountBalanceByContractAddress(string contractAddress, string accountAddress);

        #endregion

        #region Stats

        Task<double> GetBnbTotalSupply();
        Task<IEnumerable<BscValidator>> GetBscValidators();
        Task<BscBnbPrice> GetBnbLastPrice();

        #endregion

    }
}