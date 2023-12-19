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
        /// controller consutructor for initializing properties
        /// </summary>
        /// <param name="logger"></param>
        public TransactionController(ILogger<AccountController> logger, 
            ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Deposit amount in given account number
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <param name="amount">Deposit amount</param>
        /// <returns>http response</returns>
        [HttpPost]
        [Route("DepositAmount")]
        public async Task<IActionResult> DepositAmount(int accountNumber, decimal amount)
        {
            JSONResponse response = await _transactionService.DepositAmount(accountNumber, amount).ConfigureAwait(false);

            return Ok(response);
        }

        /// <summary>
        /// Withdraw amount from given account number
        /// </summary>
        /// <param name="accountNumber">unique account number</param>
        /// <param name="amount">Withdraw amount</param>
        /// <returns>http response</returns>
        [HttpPost]
        [Route("WithdrawAmount")]
        public async Task<IActionResult> WithdrawAmount(int accountNumber, decimal amount)
        {
            JSONResponse response = await _transactionService.WithdrawAmount(accountNumber, amount).ConfigureAwait(false);

            return Ok(response);
        }

    }
}
