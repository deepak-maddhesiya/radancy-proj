using Castle.Core.Logging;
using eBanking.Controllers;
using eBanking.Domain.Abstractions;
using eBanking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Test.ControllerTests
{
    public class TransactionControllerTests
    {
        private readonly Mock<ITransactionService> service;
        private readonly Mock<ILogger> loggerService;

        public TransactionControllerTests()
        {
            service = new Mock<ITransactionService>();
            loggerService = new Mock<ILogger>();
        }

        [Fact]
        public void DepositAmount_InAccount_PassingAmountAndAccountNumber()
        {
            // arrange
            var controller = new TransactionController(logger: null, service.Object);
            var actionResult = controller.DepositAmount(1, 100);

            // act
            var result = actionResult.Result;

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void WithdrawAmount_InAccount_PassingAmountAndAccountNumber()
        {
            // arrange
            var controller = new TransactionController(logger: null, service.Object);
            var actionResult = controller.WithdrawAmount(1, 100);

            // act
            var result = actionResult.Result;

            // assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
