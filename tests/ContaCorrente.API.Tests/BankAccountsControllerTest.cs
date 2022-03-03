using ContaCorrente.API.Controllers;
using ContaCorrente.Application.DTOs;
using ContaCorrente.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ContaCorrente.API.Tests
{
    public class BankAccountsControllerTest
    {
        [Fact]
        public async Task GetAccountAsync_WithUnexistingNumber_RetunsNotFound()
        {
            //Arrange
            var service = new Mock<IBankAccountService>();
            service.Setup(repo =>
                repo.GetAccountAsync("123456-1", "371", "0001"));

            var controller = new BankAccountsController(service.Object);

            // Act
            var result = await controller.GetBalance("123456-1", "371", "0001");
            
            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }        

        [Fact]
        public async Task PostAccoutAssync_ValidObjectPassed_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            BankAccountModelDTO newAccount = new BankAccountModelDTO()
            {
                BankCode = "371",
                AgencyNumber = "0001",
                AccountNumber = "998877-6",
                Balance = 78
            };

            var service = new Mock<IBankAccountService>();
            service.Setup(repo =>
                repo.Add(newAccount));

            var controller = new BankAccountsController(service.Object);

            // Act
            var createdResponse = await controller.Post(newAccount);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(createdResponse);
        }

        [Fact]
        public async Task PutAccoutAssync_InvalidaAccount_ReturnsXXX()
        {
            // Arrange
            BankAccountModelDTO newAccount = new BankAccountModelDTO()
            {
                BankCode = "371",
                AgencyNumber = "0001",
                AccountNumber = "123456-1",
                Balance = 90
            };

            var service = new Mock<IBankAccountService>();
            service.Setup(repo =>
                repo.Add(newAccount));

            var controller = new BankAccountsController(service.Object);

            // Act
            var createdResponse = await controller.Put(newAccount);

            // Assert
            Assert.IsType<NotFoundObjectResult>(createdResponse);
        }
    }
}
