using CryptoInfoApi.Controllers;
using CryptoInfoApi.Models;
using CryptoInfoApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CryptoInfoApi.Tests.Controllers
{
    public class CurrencyControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResult_WithCurrencyList()
        {
            // Arrange
            var mockRepo = new Mock<ICurrencyRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Currency> { new Currency { Id = 1, Code = "USD", ChineseName = "美元" } });
            var controller = new CurrencyController(mockRepo.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Currency>>(okResult.Value);
            Assert.Single(list);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotExist()
        {
            var mockRepo = new Mock<ICurrencyRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Currency?)null);
            var controller = new CurrencyController(mockRepo.Object);
            var result = await controller.GetById(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Add_ReturnsCreatedAtAction()
        {
            var mockRepo = new Mock<ICurrencyRepository>();
            var currency = new Currency { Id = 1, Code = "USD", ChineseName = "美元" };
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Currency>())).ReturnsAsync(currency);
            var controller = new CurrencyController(mockRepo.Object);
            var result = await controller.Add(currency);
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
