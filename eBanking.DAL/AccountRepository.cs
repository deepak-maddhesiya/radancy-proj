using eBanking.Domain.Abstractions;
using eBanking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBanking.Domain.Models;
using System.Runtime.CompilerServices;

namespace eBanking.DAL
{
    public class AccountRepository : IAccountRepository
    {
        public List<Account> _accountsRepo = In_MemoryData.accounts;

        public async Task<Account> CreateAccount(AccountDTO accountDTO)
        {
            try
            {
                int newAccountNumber;

                if (_accountsRepo.Any())
                    newAccountNumber = await Task.FromResult(_accountsRepo.Last().AccountNumber + 1).ConfigureAwait(false);
                else
                    newAccountNumber = 1;

                var newAccount = new Account()
                {
                    AccountNumber = newAccountNumber,
                    Balance = accountDTO.Balance,
                    FirstName = accountDTO.FirstName,
                    LastName = accountDTO.LastName
                };

                _accountsRepo.Add(newAccount);

                return newAccount;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Account> DeleteAccount(int AccountNumber)
        {
            try
            {
                Account userAccount = await Task.FromResult(_accountsRepo.Where(x => x.AccountNumber == AccountNumber).FirstOrDefault()!)
                    .ConfigureAwait(false);

                if (userAccount != null)
                {
                    await Task.FromResult(_accountsRepo.Remove(userAccount)).ConfigureAwait(false);
                }
                return userAccount;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Account> GetAccount(int accountNumber)
        {
            try
            {
                var userAccount = await Task.FromResult(_accountsRepo.Where(x => x.AccountNumber == accountNumber).FirstOrDefault())
                    .ConfigureAwait(false);

                return userAccount;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            try
            {
                var allAccounts = await Task.FromResult(_accountsRepo.ToList()).ConfigureAwait(false);

                return allAccounts;
            }
            catch
            {
                throw;
            }
        }
    }
}