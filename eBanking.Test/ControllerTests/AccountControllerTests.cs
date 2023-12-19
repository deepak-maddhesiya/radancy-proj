using Castle.Core.Logging;
using eBanking.Controllers;
using eBanking.DAL;
using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Domain.Models;
using eBanking.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace eBanking.Test.ControllerTests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> service;
        private readonly Mock<ILogger> loggerService;

        public AccountControllerTests()
        {
            service = new Mock<IAccountService>();
            loggerService = new Mock<ILogger>();
        }

        [Fact]
        public void GetAccounts_ListOfAccount_AccountsExistInRepo()
        {
            // arrange
            service.Setup(x => x.AllAccountInformation())
            .Returns(GetSampleAccounts);
            var controller = new AccountController(logger: null, service.Object);

            //act
            var actionResult = controller.GetAllAccounts();
            var result = actionResult.Result as OkObjectResult;
            var actual = (result.Value as JSONResponse).Body as IEnumerable<Account>;
            var expected = GetSampleAccounts().Result.Body as IEnumerable<Account>;


            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected.Count(), actual.Count());
        }

        [Fact]
        public void GetAccount_FromListOfAccount_AccountExistInRepo()
        {
            // arrange
            service.Setup(x => x.AccountInformation(1))
            .Returns(GetSampleAccount);
            var controller = new AccountController(logger: null, service.Object);

            //act
            var result = controller.GetAccount(1).Result as OkObjectResult;
            var actual = (result.Value as JSONResponse).Body as IEnumerable<Account>;

            var expected = GetSampleAccount().Result.Body as IEnumerable<Account>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected.Count(), actual.Count());
            Assert.Equal(expected.First().FirstName, actual.First().FirstName);
        }

        [Fact]
        public void GetAccount_ShouldReturnNull_AccountNotExistInRepo()
        {
            // arrange
            service.Setup(x => x.AccountInformation(1))
            .Returns(GetSampleAccount);
            var controller = new AccountController(logger: null, service.Object);

            //act
            var result = controller.GetAccount(2).Result as OkObjectResult;
            var actual = result.Value;

            //assert
            Assert.Null(actual);
        }

        [Fact]
        public void CreateAccount_OKStatus_PassingAccountObjectToCreate()
        {
            var newAccount = (GetSampleAccount().Result.Body as List<Account>).First();

            var controller = new AccountController(logger: null, service.Object);
            var actionResult = controller.CreateAccount(
                new AccountDTO()
                {
                    Balance = newAccount.Balance,
                    FirstName = newAccount.FirstName,
                    LastName = newAccount.LastName
                });

            var result = actionResult.Result;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteAccount_OKStatus_PassingAccountnumberToDelete()
        {
            // arrange
            var controller = new AccountController(logger: null, service.Object);
            
            // Act
            var actionResult = controller.DeleteAccount(1);

            var result = actionResult.Result;

            // Asserts
            Assert.IsType<OkObjectResult>(result);
        }


        private async Task<JSONResponse> GetSampleAccounts()
        {
            JSONResponse response = new JSONResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "All User Accounts details",
                Body = new List<Account>()
                {
                    new Account(){ FirstName = "Deepak", LastName = "Roger", AccountNumber = 1, Balance = 1000 },
                    new Account(){ FirstName = "Amit", LastName = "Dev", AccountNumber = 2, Balance = 4000 },
                    new Account(){ FirstName = "John", LastName = "Smith", AccountNumber = 3, Balance = 1500 }
                }
            };

            return await Task.FromResult(response);
        }

        private async Task<JSONResponse> GetSampleAccount()
        {
            JSONResponse response = new JSONResponse()
            {
                Success = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "All User Accounts details",
                Body = new List<Account>()
                {
                    new Account(){ FirstName = "Deepak", LastName = "Roger", AccountNumber = 1, Balance = 1000 },
                }
            };

            return await Task.FromResult(response);
        }
    }
}