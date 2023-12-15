using eBanking.Domain;
using eBanking.Domain.Abstractions;

namespace eBanking.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository repo)
        {
            _accountRepository = repo;
        }

        public Account AccountInformation(int AccountNumber)
        {
            try
            {
                return _accountRepository.GetAccount(AccountNumber);
            }
            catch
            {
                throw;
            }
        }

        public List<Account> AllAccountInformation()
        {
            try
            {
                return _accountRepository.GetAllAccounts();
            }
            catch
            {
                throw;
            }
        }

        public Account CloseAccount(int AccountNumber)
        {
            try
            {
                return _accountRepository.DeleteAccount(AccountNumber);
            }
            catch
            {
                throw;
            }
        }

        public Account OpenAccount(string FirstName, string LastName, decimal depositAmount)
        {
            try
            {
                return _accountRepository.CreateAccount(FirstName, LastName, depositAmount);
            }
            catch
            {
                throw;
            }
        }
    }
}