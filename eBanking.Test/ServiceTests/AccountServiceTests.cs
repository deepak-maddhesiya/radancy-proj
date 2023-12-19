using eBanking.Controllers;
using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Domain.Models;
using eBanking.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Test.ServiceTests
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> mockRepo;

        public AccountServiceTests()
        {
            mockRepo = new Mock<IAccountRepository>();
        }

        [Fact]
        public void GetAccounts_ListOfAccount_AccountsExistInRepo()
        {
            // arrange
            mockRepo.Setup(x => x.GetAllAccounts())
            .Returns(GetSampleAccounts);
            var service = new AccountService(mockRepo.Object);

            //act
            var actionResult = service.AllAccountInformation();
            var result = actionResult.Result;
            var actual = (result).Body as IEnumerable<Account>;
            var expected = GetSampleAccounts().Result as IEnumerable<Account>;


            //assert
            Assert.Equal(expected.Count(), actual?.Count());
        }

        [Fact]
        public void GetAccount_Account_AccountByAccountNumberExistInRepo()
        {
            // arrange
            mockRepo.Setup(x => x.GetAccount(1))
            .Returns(GetSampleAccount);
            var service = new AccountService(mockRepo.Object);

            //act
            var actionResult = service.AccountInformation(1);
            var result = actionResult.Result;
            var actual = (result).Body as Account;
            var expected = GetSampleAccount().Result;


            //assert
            Assert.Equal(expected.AccountNumber, actual?.AccountNumber);
        }

        [Fact]
        public void GetAccount_Account_AccountByInValidAccountNumber()
        {
            // arrange
            mockRepo.Setup(x => x.GetAccount(1))
            .Returns(GetSampleAccount);
            var service = new AccountService(mockRepo.Object);

            //act
            var actionResult = service.AccountInformation(2);
            var result = actionResult.Result;
            var actual = (result).Body as Account;
            var expected = GetSampleAccount().Result;


            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void OpenAccount_PassingAccountObjectToCreate()
        {
            // arrange
            var service = new AccountService(mockRepo.Object);
            AccountDTO accountDTO = new AccountDTO()
            {
                Balance = 100,
                FirstName = "Test",
                LastName = "Test"
            };

            //act
            var actionResult = service.OpenAccount(accountDTO);
            var result = actionResult.Result;

            //assert
            Assert.True(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void OpenAccount_PassingAccountObjectWithAmountLessThanHundredToCreate()
        {
            // arrange
            var service = new AccountService(mockRepo.Object);
            AccountDTO accountDTO = new AccountDTO()
            {
                Balance = 10,
                FirstName = "Test",
                LastName = "Test"
            };

            //act
            var actionResult = service.OpenAccount(accountDTO);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void OpenAccount_PassingAccountObjectWithAmountGreaterThanThousandToCreate()
        {
            // arrange
            var service = new AccountService(mockRepo.Object);
            AccountDTO accountDTO = new AccountDTO()
            {
                Balance = 10001,
                FirstName = "Test",
                LastName = "Test"
            };

            //act
            var actionResult = service.OpenAccount(accountDTO);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void DeleteAccount_PassingValidAccountNumberToDelete()
        {
            // arrange
            var entity = GetSampleAccount();

            mockRepo.Setup(x => x.DeleteAccount(1)).Returns(entity);
            var service = new AccountService(mockRepo.Object);
            
            //act
            var actionResult = service.CloseAccount(1);
            var result = actionResult.Result;

            //assert
            Assert.True(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void DeleteAccount_PassingInValidAccountNumberToDelete()
        {
            // arrange
            var entity = GetSampleAccount();

            mockRepo.Setup(x => x.DeleteAccount(1)).Returns(entity);
            var service = new AccountService(mockRepo.Object);

            //act
            var actionResult = service.CloseAccount(3);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        private async Task<List<Account>> GetSampleAccounts()
        {
            List<Account> response = new List<Account>()
                {
                    new Account(){ FirstName = "Deepak", LastName = "Roger", AccountNumber = 1, Balance = 1000 },
                    new Account(){ FirstName = "Amit", LastName = "Dev", AccountNumber = 2, Balance = 4000 },
                    new Account(){ FirstName = "John", LastName = "Smith", AccountNumber = 3, Balance = 1500 }
                };

            return await Task.FromResult(response);
        }

        private async Task<Account> GetSampleAccount()
        {
            Account response = new Account()
            {
                FirstName = "Deepak",
                LastName = "Roger",
                AccountNumber = 1,
                Balance = 1000
            };

            return await Task.FromResult(response);
        }
    }
}
