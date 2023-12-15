using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Service;
using Microsoft.AspNetCore.Mvc;

namespace eBanking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// application logger
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Account service object
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// controller consutructor for initializing properties
        /// </summary>
        /// <param name="logger"></param>
        public AccountController(ILogger<AccountController> logger, IAccountService accountService, ITransactionService transactionService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// get all user accounts 
        /// </summary>
        /// <returns>http response</returns>
        [HttpGet]
        [Route("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            JSONResponse response;

            try
            {
                var allAccounts = _accountService.AllAccountInformation();

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
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User account does not exist."
                    };
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

        /// <summary>
        /// Gte user account details for specific account number
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <returns>http response</returns>
        [HttpGet]
        [Route("GetAccountByAccountNumber")]
        public IActionResult GetAccount(int accountNumber)
        {
            JSONResponse response;

            try
            {
                var userAccount = _accountService.AccountInformation(accountNumber);

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
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User Account details",
                        Body = userAccount
                    };
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
                };

                return BadRequest(response);
            }
        }

        /// <summary>
        /// This method used for creating account using parameters
        /// </summary>
        /// <param name="FirstName">User first name</param>
        /// <param name="LastName">User last name</param>
        /// <param name="depositAmount">depposit amount</param>
        /// <returns>http response</returns>
        [HttpPost]
        [Route("CreateAccount")]
        public IActionResult CreateAccount(string FirstName, string LastName, decimal depositAmount)
        {
            JSONResponse response;

            try
            {
                //check for deposit less than $100
                if (depositAmount < 100)
                {
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User cannot deposit less than $100.",
                    };

                    return Ok(response);
                }

                //check for deposit more than $10,000
                if (depositAmount > 10000)
                {
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "User cannot deposit more than $10,000.",
                    };

                    return Ok(response);
                }

                var newAccount = _accountService.OpenAccount(FirstName, LastName, depositAmount);

                response = new JSONResponse()
                {
                    Success = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Below account added successfully",
                    Body = newAccount
                };

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

        /// <summary>
        /// This method used for delete account
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <returns>http response</returns>
        [HttpPost]
        [Route("DeleteAccountByAccountNumber")]
        public IActionResult DeleteAccount(int accountNumber)
        {
            JSONResponse response;

            try
            {
                Account userAccount = _accountService.CloseAccount(accountNumber);

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
                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Account deleted successfully"
                    };
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
