using eBanking.Domain.Abstractions;
using eBanking.Domain;

namespace eBanking.DAL
{
    public class TransactionRepository : ITransactionRepository
    {
        public List<Account> _transactionRepo = In_MemoryData.accounts;

        public async Task<Account> DepositAmount(int accountNumber, decimal amount)
        {
            try
            {
                var userAccount = await Task.FromResult(_transactionRepo
                    .FirstOrDefault(x => x.AccountNumber == accountNumber)!).ConfigureAwait(false);

                userAccount.Balance += amount;

                return userAccount;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Account> WithdrawAmount(int accountNumber, decimal amount)
        {
            try
            {
                var userAccount = await Task.FromResult(_transactionRepo.FirstOrDefault(x => x.AccountNumber == accountNumber)!).ConfigureAwait(false);

                userAccount.Balance -= amount;

                return userAccount;
            }
            catch
            {
                throw;
            }
        }
    }
}
