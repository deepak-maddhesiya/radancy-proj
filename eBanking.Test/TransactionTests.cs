using eBanking.DAL;
using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Service;
using Moq;

namespace eBanking.Test
{
    public class TransactionTests
    {
        private ITransactionService? _transactionService;
        private ITransactionRepository _transactionRepository = new TransactionRepository();

        [Fact]
        public void Deposit_Should_ReturnNull_WhenValueIsInvalid()
        {
            // Arrange
            _transactionService = new TransactionService(_transactionRepository);

            Account value = new Account()
            {
                AccountNumber = 1,
                Balance = 500,
                FirstName = "deepak",
                LastName = "Maddhesiya"
            };

            // Act
            var result = _transactionService.DepositAmount(5, 100);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Deposit_ShouldNot_ReturnNull_WhenValueIsValid()
        {
            // Arrange
            _transactionService = new TransactionService(_transactionRepository);

            Account value = new Account()
            {
                AccountNumber = 1,
                Balance = 500,
                FirstName = "deepak",
                LastName = "Maddhesiya"
            };

            // Act
            var result = _transactionService.DepositAmount(1, 100);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(value.AccountNumber, result.AccountNumber);
        }

        [Fact]
        public void Withdrow_Should_ReturnNull_WhenValueIsInvalid()
        {
            // Arrange
            _transactionService = new TransactionService(_transactionRepository);

            Account value = new Account()
            {
                AccountNumber = 1,
                Balance = 500,
                FirstName = "deepak",
                LastName = "Maddhesiya"
            };

            // Act
            var result = _transactionService.WithdrawAmount(5, 100);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Withdrow_ShouldNot_ReturnNull_WhenValueIsValid()
        {
            // Arrange
            _transactionService = new TransactionService(_transactionRepository);

            Account value = new Account()
            {
                AccountNumber = 1,
                Balance = 500,
                FirstName = "deepak",
                LastName = "Maddhesiya"
            };

            // Act
            var result = _transactionService.DepositAmount(1, 100);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(value.AccountNumber, result.AccountNumber);
        }
    }
}