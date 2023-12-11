using Microsoft.AspNetCore.Mvc;
using Radancy_Code.Models;

namespace Radancy_Code.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// in-memory data structure(replace with db data)
        /// </summary>
        private static readonly List<Account> accounts = new List<Account>()
        {
            new Account(){ FirstName = "John", LastName = "Roger", AccountNumber = 1, Balance = 1000 },
            new Account(){ FirstName = "Den", LastName = "Smith", AccountNumber = 2, Balance = 4000 },
            new Account(){ FirstName = "Joy", LastName = "Smith", AccountNumber = 3, Balance = 1500 }
        };

        /// <summary>
        /// application logger
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// controller consutructor for initializing properties
        /// </summary>
        /// <param name="logger"></param>
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
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
                var allAccounts = accounts.ToList();

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
                var userAccount = accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();

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

                int newAccountNumber;

                if (accounts.Any())
                    newAccountNumber = accounts.Last().AccountNumber + 1;
                else
                    newAccountNumber = 1;

                Account newAccount = new Account()
                {
                    AccountNumber = newAccountNumber,
                    Balance = depositAmount,
                    FirstName = FirstName,
                    LastName = LastName
                };

                accounts.Add(newAccount);

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
                Account userAccount = accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault()!;

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
                    accounts.Remove(userAccount);

                    response = new JSONResponse()
                    {
                        Success = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Below account deleted successfully"
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
                Account userAccount = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber)!;

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
                        userAccount.Balance += amount;

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
                Account userAccount = accounts.FirstOrDefault(x => x.AccountNumber == accountNumber)!;

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
                        userAccount.Balance -= amount;

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
