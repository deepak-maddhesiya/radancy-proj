using eBanking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Domain.Abstractions
{
    public interface IAccountRepository
    {
        Task<Account> GetAccount(int AccountNumber);

        Task<List<Account>> GetAllAccounts();

        Task<Account> CreateAccount(AccountDTO accountDTO);

        Task<Account> DeleteAccount(int AccountNumber);

    }
}
