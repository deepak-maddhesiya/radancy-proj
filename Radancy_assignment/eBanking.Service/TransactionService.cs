using eBanking.Domain;
using eBanking.Domain.Abstractions;

namespace eBanking.Service
{
    /// <summary>
    /// Transaction
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository repo)
        {
            _transactionRepository = repo;
        }

        public Account DepositAmount(int accountNumber, decimal amount)
        {
            try
            {
                return _transactionRepository.DepositAmount(accountNumber, amount);
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
                return _transactionRepository.WithdrawAmount(accountNumber, amount);
            }
            catch
            {
                throw;
            }
        }
    }
}