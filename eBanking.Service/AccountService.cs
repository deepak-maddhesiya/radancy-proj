using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Domain.Models;
using System.Security.Principal;

namespace eBanking.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository repo)
        {
            _accountRepository = repo;
        }

        /// <summary>
        /// get account information based on account number
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        public async Task<JSONResponse> AccountInformation(int AccountNumber)
        {
            JSONResponse response;

            try
            {
                var userAccount = await _accountRepository.GetAccount(AccountNumber);

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
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User Account details",
                        Body = userAccount
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
        /// get all account information
        /// </summary>
        /// <returns></returns>
        public async Task<JSONResponse> AllAccountInformation()
        {
            JSONResponse response;

            try
            {
                var allAccounts = await _accountRepository.GetAllAccounts().ConfigureAwait(false);
                
                if (allAccounts.Any())
                {
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "All User Accounts details",
                        Body = allAccounts
                    };
                }
                else
                {
                    response = new JSONResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User account does not exist."
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
        /// close account based on account number
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        public async Task<JSONResponse> CloseAccount(int AccountNumber)
        {
            JSONResponse response;

            try
            {
                var userAccount = await _accountRepository.DeleteAccount(AccountNumber).ConfigureAwait(false);

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
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Account deleted successfully"
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
        /// open account based on account DTO 
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        public async Task<JSONResponse> OpenAccount(AccountDTO accountDTO)
        {
            JSONResponse response;

            try
            {
                //check for deposit less than $100
                if (accountDTO.Balance < 100)
                {
                    response = new JSONResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User cannot deposit less than $100.",
                    };

                    return response;
                }

                //check for deposit more than $10,000
                if (accountDTO.Balance > 10000)
                {
                    response = new JSONResponse()
                    {
                        Success = false,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User cannot deposit more than $10,000.",
                    };

                    return response;
                }

                var newAccount = await _accountRepository.CreateAccount(accountDTO).ConfigureAwait(false);

                response = new JSONResponse()
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Below account added successfully",
                    Body = newAccount
                };
            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}