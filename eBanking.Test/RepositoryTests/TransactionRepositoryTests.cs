using eBanking.DAL;
using eBanking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Test.RepositoryTests
{
    public class TransactionRepositoryTests
    {
        [Fact]
        public async Task DepositAmount_Transaction_DepositByAccountNumberAndAmount()
        {
            // arrange
            var transactionRepository = new TransactionRepository();
            transactionRepository._transactionRepo = GetSampleAccounts();

            // Act
            var result = await transactionRepository.DepositAmount(1, 1000);
            var expected = GetSampleAccounts().Where(x => x.AccountNumber == 1).First();
            expected.Balance += 1000;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(expected.AccountNumber, result.AccountNumber);
            Assert.Equal(expected.Balance, result.Balance);
        }

        [Fact]
        public async Task WithdrawAmount_Transaction_WithdrawByAccountNumberAndAmount()
        {
            // arrange
            var transactionRepository = new TransactionRepository();
            transactionRepository._transactionRepo = GetSampleAccounts();

            // Act
            var result = await transactionRepository.WithdrawAmount(1, 100);
            var expected = GetSampleAccounts().Where(x => x.AccountNumber == 1).First();
            expected.Balance -= 100;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Account>(result);
            Assert.Equal(expected.AccountNumber, result.AccountNumber);
            Assert.Equal(expected.Balance, result.Balance);
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
