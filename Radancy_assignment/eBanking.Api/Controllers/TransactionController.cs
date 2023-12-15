using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Service;
using Microsoft.AspNetCore.Mvc;

namespace eBanking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        /// <summary>
        /// application logger
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Transaction Service Object
        /// </summary>
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Transaction Service Object
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// controller consutructor for initializing properties
        /// </summary>
        /// <param name="logger"></param>
        public TransactionController(ILogger<AccountController> logger, 
            ITransactionService transactionService, IAccountService accountService)
        {
            _logger = logger;
            _transactionService = transactionService;
            _accountService = accountService;
        }

        /// <summary>
        /// Deposit amount in given account number
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <param name="amount">Deposit amount</param>
        /// <returns>http response</returns>
        [HttpPost]
        [Route("DepositAmount")]
        public IActionResult DepositAmount(int accountNumber, decimal amount)
        {
            JSONResponse response;

            try
            {
                Account userAccount = _accountService.AccountInformation(accountNumber);

                if (userAccount == null)
                {
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User account does not exist."
                    };
                }
                else
                {
                    if (amount > 10000)
                    {
                        response = new JSONResponse()
                        {
                            Success = true,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = "User cannot deposit more than $10,000 in a single transaction.",
                        };
                    }
                    else
                    {

                        userAccount = _transactionService.DepositAmount(accountNumber, amount);

                        response = new JSONResponse()
                        {
                            Success = true,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = "Amount deposited successfully in below account",
                            Body = userAccount
                        };
                    }
                }
                return Ok(response);
            }
            catch
            {
                // Handling for unforeseen error
                response = new JSONResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error",
                    Body = ""
                };

                return BadRequest(response);
            }

        }

        /// <summary>
        /// Withdraw amount from given account number
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <param name="amount">Withdraw amount</param>
        /// <returns>http response</returns>
        [HttpPost]
        [Route("WithdrawAmount")]
        public IActionResult WithdrawAmount(int accountNumber, decimal amount)
        {
            JSONResponse response;

            try
            {
                Account userAccount = _accountService.AccountInformation(accountNumber);

                if (userAccount == null)
                {
                    response = new JSONResponse()
                    {
                        Success = true,
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
                            Success = true,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = "Insufficient balance.",
                        };
                    }
                    else if (userAccount.Balance - amount < 100)
                    {
                        response = new JSONResponse()
                        {
                            Success = true,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = $"Your account balance is {userAccount.Balance}. Account cannot have less than $100 at any time in an account.",
                        };
                    }
                    else if (nintypercent < amount)
                    {
                        response = new JSONResponse()
                        {
                            Success = true,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = $"Your account balance is {userAccount.Balance}. user cannot withdraw more than 90% of their total balance from an account in a single transaction.",
                        };
                    }
                    else
                    {
                        userAccount = _transactionService.WithdrawAmount(accountNumber, amount);

                        response = new JSONResponse()
                        {
                            Success = true,
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Message = "Amount debited successfully from below account",
                            Body = userAccount
                        };   
                    }
                }
                return Ok(response);
            }
            catch
            {
                // Handling for unforeseen error
                response = new JSONResponse()
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error"
                };

                return BadRequest(response);
            }

        }

    }
}
