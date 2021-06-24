using System.Collections.Generic;
using System.Threading.Tasks;
using BscScanner.Data;

namespace BscScanner {
    public interface IBscScanClient {
        Task<float> GetBnbBalanceSingleAsync(string address);
        Task<IEnumerable<BnbBalance>> GetBnbBalanceMultipleAsync(IEnumerable<string> addresses);
        Task<IEnumerable<BscTransaction>> GetTransactionsByAddress(string address, int startBlock = 1,
            int endBlock = 99999999);
        Task<IEnumerable<BscTransaction>> GetTransactionsByHash(string hash);
        Task<IEnumerable<BscTransaction>> GetTransactionsByBlockRange(int startBlock = 1, int endBlock = 99999999);
        Task<IEnumerable<BscTransaction>> GetBep20TokenTransfersByAddress(string address);
        Task<IEnumerable<BscTransaction>> GetErc721TokenTransfersByAddress(string address);
        Task<IEnumerable<BscBlock>> GetBlocksValidatedByAddress(string address);

        Task<string> GetAbiFromSourceAddress(string address);
        Task<IEnumerable<BscContract>> GetSourceCodeFromSourceAddress(string address);

        Task<TxStatus> GetTransactionReceiptStatus(string txHash);

        Task<BscBlockReward> GetBlockRewardByBlock(int block);
        Task<BscBlockCountdown> GetBlockCountdownByBlock(int block);
    }
}