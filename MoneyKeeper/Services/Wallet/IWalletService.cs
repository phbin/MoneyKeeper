using MoneyKeeper.Dtos;
using MoneyKeeper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyKeeper.Services
{
    public interface IWalletService
    {
        public Task<IEnumerable<Wallet>> GetWallets(int userId);
        public Task<Wallet> CreateWallet(int userId, CreateWalletDto walletDto);
        public Task UpdateWallet(int walletId, int userid, UpdateWalletDto updateWallet);
        public Task UpdateWalletBalance(int walletId, int amount);
        Task<bool> VerifyIsUserInWallet(int walletId, int userId);
        Task DeleteWallet(int walletId, int userId);
        Task<IEnumerable<WalletMember>> GetUsersInWallet(int walletId, int userId);
        //Task DeleteMemberInWallet(int userId, int walletId, int memberId);
        Task AddMemberToWallet(int userId, int walletId, int memberId);
        Task RemoveMemberFromWallet(int userId, int walletId, int memberId);
    }
}
