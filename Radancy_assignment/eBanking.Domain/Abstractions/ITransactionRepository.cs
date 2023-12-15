using eBanking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Domain.Abstractions
{
    public interface ITransactionRepository
    {
        Account DepositAmount(int accountNumber, decimal amount);

        Account WithdrawAmount(int accountNumber, decimal amount);
    }
}
