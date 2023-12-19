using eBanking.Domain;
using eBanking.Domain.Abstractions;

namespace eBanking.Service
{
    /// <summary>
    /// Transaction
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository repo, IAccountRepository accRepo)
        {
            _transactionRepository = repo;
            _accountRepository = accRepo;
        }

        /// <summary>
        /// Deposit amount
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<JSONResponse> DepositAmount(int accountNumber, decimal amount)
        {
            JSONResponse response;

            try
            {
                var userAccount = await _accountRepository.GetAccount(accountNumber).ConfigureAwait(false);

                if (userAccount == null)
                {
                    response = new JSONResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User account does not exist."
                    };
                }
                else if (amount > 10000)
                {
                    response = new JSONResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User cannot deposit more than $10,000 in a single transaction.",
                    };
                }
                else
                {
                    var updatedUserAccount = await _transactionRepository.DepositAmount(accountNumber, amount).ConfigureAwait(false);

                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Amount deposited successfully in below account",
                        Body = updatedUserAccount
                    };
                }
            }
            catch
            {
                throw;
            }

            return response;
        }

        /// <summary>
        /// Withdraw amount
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<JSONResponse> WithdrawAmount(int accountNumber, decimal amount)
        {
            JSONResponse response;

            try
            {
                var userAccount = await _accountRepository.GetAccount(accountNumber).ConfigureAwait(false);

                if (userAccount == null)
                {
                    response = new JSONResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User account does not exist."
                    };
                }
                else
                {
                    decimal nintypercent = (userAccount.Balance * 90) / 100;

                    if (userAccount.Balance < amount)
                    {
                        response = new JSONResponse()
                        {
                            Success = false,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = "Insufficient balance.",
                        };
                    }
                    else if (userAccount.Balance - amount < 100)
                    {
                        response = new JSONResponse()
                        {
                            Success = false,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = $"Your account balance is {userAccount.Balance}. Account cannot have less than $100 at any time in an account.",
                        };
                    }
                    else if (nintypercent < amount)
                    {
                        response = new JSONResponse()
                        {
                            Success = false,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = $"Your account balance is {userAccount.Balance}. user cannot withdraw more than 90% of their total balance from an account in a single transaction.",
                        };
                    }
                    else
                    {
                        userAccount = await _transactionRepository.WithdrawAmount(accountNumber, amount).ConfigureAwait(false);

                        response = new JSONResponse()
                        {
                            Success = true,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = "Amount debited successfully from below account",
                            Body = userAccount
                        };
                    }
                }
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}