using eBanking.Controllers;
using eBanking.DAL;
using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Test.RepositoryTests
{
    public class AccountRepositoryTests
    {
        [Fact]
        public async Task GetAllAccounts_Account_ListOfAccount()
        {
            // arrange
            var inMemoryData = GetSampleAccounts(); 
            var accountRepository = new AccountRepository(); 
            accountRepository._accountsRepo = inMemoryData;

            // Act
            var result = await accountRepository.GetAllAccounts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(inMemoryData, result);
        }

        [Fact]
        public async Task GetAccount_Account_AccountByAccountNumber()
        {
            // arrange
            var inMemoryData = GetSampleAccounts();
            var accountRepository = new AccountRepository();
            accountRepository._accountsRepo = inMemoryData;

            // Act
            var result = await accountRepository.GetAccount(1);
            var expected = inMemoryData.Where(x => x.AccountNumber == 1).First();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task DeleteAccount_Account_DeleteByAccountNumber()
        {
            // arrange
            var accountRepository = new AccountRepository();
            accountRepository._accountsRepo = GetSampleAccounts();

            // Act
            var result = await accountRepository.DeleteAccount(1);
            var expected = GetSampleAccounts().Where(x=>x.AccountNumber == 1).First();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(expected.AccountNumber, result.AccountNumber);
        }

        [Fact]
        public async Task CreateAccount_Account_CreateAccountByObject()
        {
            // arrange
            var accountRepository = new AccountRepository();
            accountRepository._accountsRepo = GetSampleAccounts();

            var accountDTO = new AccountDTO() { FirstName = "Test", LastName = "Test", Balance = 1500 };

            int newAccountNumber;
            if (GetSampleAccounts().Any())
                newAccountNumber = await Task.FromResult(GetSampleAccounts().Last().AccountNumber + 1).ConfigureAwait(false);
            else
                newAccountNumber = 1;

            Account addNew = new Account() { AccountNumber = newAccountNumber, FirstName = accountDTO.FirstName, LastName = accountDTO.LastName, Balance = accountDTO.Balance };

            // Act
            var result = await accountRepository.CreateAccount(accountDTO);
            var expected = addNew;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(expected.AccountNumber, result.AccountNumber);
        }

        private List<Account> GetSampleAccounts()
        {
            List<Account> response = new List<Account>()
                {
                    new Account(){ FirstName = "Deepak", LastName = "Roger", AccountNumber = 1, Balance = 1000 },
                    new Account(){ FirstName = "Amit", LastName = "Dev", AccountNumber = 2, Balance = 4000 },
                    new Account(){ FirstName = "John", LastName = "Smith", AccountNumber = 3, Balance = 1500 }
                };

            return response;
        }

    }
}
