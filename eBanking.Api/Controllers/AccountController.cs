using eBanking.Domain;
using eBanking.Domain.Abstractions;
using eBanking.Domain.Models;
using eBanking.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

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
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
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
        public async Task<IActionResult> GetAllAccounts()
        {
            JSONResponse response = await _accountService.AllAccountInformation().ConfigureAwait(false);

            return Ok(response);
        }

        /// <summary>
        /// Gte user account details for specific account number
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <returns>http response</returns>
        [HttpGet]
        [Route("GetAccountByAccountNumber")]
        public async Task<IActionResult> GetAccount(int accountNumber)
        {
            JSONResponse response = await _accountService.AccountInformation(accountNumber).ConfigureAwait(false);

            return Ok(response);

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
        public async Task<IActionResult> CreateAccount(AccountDTO accountDTO)
        {
            JSONResponse response = await _accountService.OpenAccount(accountDTO).ConfigureAwait(false);

            return Ok(response);
        }

        /// <summary>
        /// This method used for delete account
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <returns>http response</returns>
        [HttpPost]
        [Route("DeleteAccountByAccountNumber")]
        public async Task<IActionResult> DeleteAccount(int accountNumber)
        {
            JSONResponse response = await _accountService.CloseAccount(accountNumber).ConfigureAwait(false);

            return Ok(response);
        }
    }
}
