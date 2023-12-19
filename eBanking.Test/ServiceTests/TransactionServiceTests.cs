using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Test.ServiceTests
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> mockTransactionRepo;
        private readonly Mock<IAccountRepository> mockAccountRepo;

        public TransactionServiceTests()
        {
            mockTransactionRepo = new Mock<ITransactionRepository>();
            mockAccountRepo = new Mock<IAccountRepository>();
        }

        [Fact]
        public void DepositAmount_Transaction_InValidAccountNumber()
        {
            // arrange
            var entity = GetSampleAccount();
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);
            
            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.DepositAmount(3, 1000);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void DepositAmount_Transaction_AmountGreaterThanThousand()
        {
            // arrange
            var entity = GetSampleAccount();
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);

            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.DepositAmount(1, 10001);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void DepositAmount_Transaction_ValidAccountNumberAndAmount()
        {
            // arrange
            var entity = GetSampleAccount();
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);
            
            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.DepositAmount(1, 1000);
            var result = actionResult.Result;

            //assert
            Assert.True(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void WithdrawAmount_Transaction_InValidAccountNumber()
        {
            // arrange
            var entity = GetSampleAccount();
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);

            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.WithdrawAmount(3, 1000);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void WithdrawAmount_Transaction_AccountBalanceLessThanAmount()
        {
            // arrange
            var entity = GetSampleAccount();
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);

            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.WithdrawAmount(1, 10001);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void WithdrawAmount_Transaction_AccountBalanceLessThanHundredAfterWithdraw()
        {
            // arrange
            var entity = GetSampleAccount();
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);

            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.WithdrawAmount(1, 950);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void WithdrawAmount_Transaction_AccountBalanceLessThanNintyPercentAfterWithdraw()
        {
            // arrange
            var entity = GetSampleAccount();
            entity.Result.Balance = 2000; 
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);

            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.WithdrawAmount(1, 1801);
            var result = actionResult.Result;

            //assert
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void WithdrawAmount_Transaction_PassingValidAccountAndAmount()
        {
            // arrange
            var entity = GetSampleAccount();
            entity.Result.Balance = 2000;
            mockAccountRepo.Setup(x => x.GetAccount(1)).Returns(entity);

            var service = new TransactionService(mockTransactionRepo.Object, mockAccountRepo.Object);

            //act
            var actionResult = service.WithdrawAmount(1, 1800);
            var result = actionResult.Result;

            //assert
            Assert.True(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
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
