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
    public class CryptoInfoControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WithCryptoInfoResult()
        {
            // Arrange
            var mockService = new Mock<Services.ICoindeskService>();
            var mockRepo = new Mock<ICurrencyRepository>();
            var coindeskJson = """
            {"time":{"updatedISO":"2022-08-03T20:25:00+00:00"},"bpi":{"USD":{"code":"USD","rate":"23,342.0112","rate_float":23342.0112},"GBP":{"code":"GBP","rate":"19,504.3978","rate_float":19504.3978},"EUR":{"code":"EUR","rate":"22,738.5269","rate_float":22738.5269}}}
            """;
            mockService.Setup(s => s.GetCurrentPriceJsonAsync()).ReturnsAsync(coindeskJson);
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Currency> {
                new Currency { Code = "USD", ChineseName = "美元" },
                new Currency { Code = "GBP", ChineseName = "英鎊" },
                new Currency { Code = "EUR", ChineseName = "歐元" }
            });
            var controller = new CryptoInfoApi.Controllers.CryptoInfoController(mockService.Object, mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<CryptoInfoResult>(okResult.Value);
            Assert.Equal(3, data.Currencies.Count);
            Assert.Equal("美元", data.Currencies[0].ChineseName);
            Assert.Equal(23342.0112m, data.Currencies[0].Rate);
        }
    }
}
