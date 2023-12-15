using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Domain.Abstractions
{
    public interface IAccountRepository
    {
        Account GetAccount(int AccountNumber);

        List<Account> GetAllAccounts();

        Account CreateAccount(string FirstName, string LastName, decimal depositAmount);

        Account DeleteAccount(int AccountNumber);

    }
}
