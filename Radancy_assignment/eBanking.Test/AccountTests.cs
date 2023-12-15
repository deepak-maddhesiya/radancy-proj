using eBanking.DAL;
using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Service;
using Microsoft.AspNetCore.Rewrite;

namespace eBanking.Test
{
    public class AccountTest
    {
        private IAccountService? _accountService;
        private IAccountRepository _accountRepository = new AccountRepository();

        [Fact]
        public void CreateAccount_ShouldNot_ReturnNull_WhenValueIsValid()
        {
            // Arrange
            _accountService = new AccountService(_accountRepository);

            // Act
            var result = _accountService.OpenAccount("John", "Doe", 100);

            // Assert
            Assert.NotNull(result);
            Assert.True(result != null || result.AccountNumber != 0);
        }

        [Fact]
        public void DeleteAccount_Should_ReturnEquel_WhenValueIsValid()
        {
            // Arrange
            _accountService = new AccountService(_accountRepository);

            // Act
            var result = _accountService.OpenAccount("John", "Doe", 100);

            // Assert
            Assert.NotNull(result);
            Assert.True(result != null || result.AccountNumber != 0);
        }

        [Fact]
        public void AllAccountInformation_ShouldNot_ReturnNull_WhenValueIsValid()
        {
            // Arrange
            List<Account> value = new List<Account>()
            {
                new Account(){ FirstName = "John", LastName = "Roger", AccountNumber = 1, Balance = 1000 },
                new Account(){ FirstName = "Den", LastName = "Smith", AccountNumber = 2, Balance = 4000 },
                new Account(){ FirstName = "Joy", LastName = "Smith", AccountNumber = 3, Balance = 1500 }
            };
            _accountService = new AccountService(_accountRepository);

            // Act
            List<Account> result = _accountService.AllAccountInformation();

            // Assert
            Assert.NotNull(result);
            Assert.True(result != null && result.Count > 0);
        }

        [Fact]
        public void AccountInformation_ShouldNot_ReturnNull_WhenValueIsValid()
        {
            // Arrange
            Account expected = new Account()
            {
                FirstName = "John",
                LastName = "Roger",
                AccountNumber = 1,
                Balance = 1000
            };

            _accountService = new AccountService(_accountRepository);

            // Act
            Account result = _accountService.AccountInformation(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Convert.ToInt32(expected.AccountNumber), Convert.ToInt32(result.AccountNumber));
        }
    }
}