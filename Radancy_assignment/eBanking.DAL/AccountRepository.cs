using eBanking.Domain.Abstractions;
using eBanking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eBanking.DAL
{
    public class AccountRepository : IAccountRepository
    {
        public Account CreateAccount(string FirstName, string LastName, decimal depositAmount)
        {
            try
            {
                int newAccountNumber;

                if (In_MemoryData.accounts.Any())
                    newAccountNumber = In_MemoryData.accounts.Last().AccountNumber + 1;
                else
                    newAccountNumber = 1;

                Account newAccount = new Account()
                {
                    AccountNumber = newAccountNumber,
                    Balance = depositAmount,
                    FirstName = FirstName,
                    LastName = LastName
                };

                In_MemoryData.accounts.Add(newAccount);

                return newAccount;
            }
            catch
            {
                throw;
            }
        }

        public Account DeleteAccount(int AccountNumber)
        {
            try
            {
                Account userAccount = In_MemoryData.accounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefault()!;

                if (userAccount != null)
                {
                    In_MemoryData.accounts.Remove(userAccount);
                }
                return userAccount;
            }
            catch
            {
                throw;
            }
        }

        public Account GetAccount(int accountNumber)
        {
            try
            {
                var userAccount = In_MemoryData.accounts.Where(x => x.AccountNumber == accountNumber)
                    .FirstOrDefault();

                return userAccount;
            }
            catch
            {
                throw;
            }
        }

        public List<Account> GetAllAccounts()
        {
            try
            {
                var allAccounts = In_MemoryData.accounts.ToList();

                return allAccounts;
            }
            catch
            {
                throw;
            }
        }
    }
}