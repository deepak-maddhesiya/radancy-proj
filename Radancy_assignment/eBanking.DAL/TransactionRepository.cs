using eBanking.Domain.Abstractions;
using eBanking.Domain;

namespace eBanking.DAL
{
    public class TransactionRepository : ITransactionRepository
    {
        public Account DepositAmount(int accountNumber, decimal amount)
        {
            try
            {
                Account userAccount = In_MemoryData.accounts
                    .FirstOrDefault(x => x.AccountNumber == accountNumber)!;

                userAccount.Balance += amount;

                return userAccount;
            }
            catch
            {
                throw;
            }
        }

        public Account WithdrawAmount(int accountNumber, decimal amount)
        {
            try
            {
                Account userAccount = In_MemoryData.accounts.FirstOrDefault(x => x.AccountNumber == accountNumber)!;

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
