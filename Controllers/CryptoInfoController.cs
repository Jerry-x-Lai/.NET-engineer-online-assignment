using CryptoInfoApi.Models;
using CryptoInfoApi.Repositories;
using CryptoInfoApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CryptoInfoApi.Controllers
{
    
    /// <summary>
    /// 提供加密貨幣匯率資訊 API，整合 Coindesk API 及本地幣別資料。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoInfoController : ControllerBase
    {
        private readonly ICoindeskService _coindeskService;
        private readonly ICurrencyRepository _currencyRepo;

        public CryptoInfoController(ICoindeskService coindeskService, ICurrencyRepository currencyRepo)
        {
            _coindeskService = coindeskService;
            _currencyRepo = currencyRepo;
        }

        /// <summary>
        /// 取得最新加密貨幣匯率資訊（含中文名稱、匯率、更新時間）。
        /// </summary>
        /// <returns>加密貨幣匯率資訊</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var json = await _coindeskService.GetCurrentPriceJsonAsync();
            var coindesk = JsonSerializer.Deserialize<CoindeskResponse>(json);
            if (coindesk == null) return StatusCode(500, "Coindesk API 解析失敗");
            var dbCurrencies = (await _currencyRepo.GetAllAsync()).ToList();
            var result = new CryptoInfoResult
            {
                UpdatedTime = DateTime.Parse(coindesk.Time.UpdatedISO).ToString("yyyy/MM/dd HH:mm:ss"),
                Currencies = new List<CryptoInfoCurrency>()
            };
            foreach (var bpi in new[] { coindesk.Bpi.USD, coindesk.Bpi.GBP, coindesk.Bpi.EUR })
            {
                if (bpi == null) continue;
                var db = dbCurrencies.FirstOrDefault(x => x.Code == bpi.Code);
                result.Currencies.Add(new CryptoInfoCurrency
                {
                    Code = bpi.Code,
                    ChineseName = db?.ChineseName ?? string.Empty,
                    Rate = bpi.RateFloat
                });
            }
            return Ok(result);
        }
    }
}
