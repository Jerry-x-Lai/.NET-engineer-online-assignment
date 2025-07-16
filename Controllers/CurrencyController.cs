using CryptoInfoApi.Models;
using CryptoInfoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CryptoInfoApi.Controllers
{
    /// <summary>
    /// 幣別資料 CRUD API。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _repo;

        public CurrencyController(ICurrencyRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 取得所有幣別資料。
        /// </summary>
        /// <returns>幣別清單</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// 依據主鍵取得幣別資料。
        /// </summary>
        /// <param name="id">幣別主鍵</param>
        /// <returns>幣別資料或 404</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// 新增幣別資料。
        /// </summary>
        /// <param name="currency">幣別物件</param>
        /// <returns>新增後的幣別資料</returns>
        [HttpPost]
        public async Task<IActionResult> Add(Currency currency)
        {
            var result = await _repo.AddAsync(currency);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// 更新幣別資料。
        /// </summary>
        /// <param name="id">幣別主鍵</param>
        /// <param name="currency">幣別物件</param>
        /// <returns>更新後的幣別資料或 404</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Currency currency)
        {
            if (id != currency.Id) return BadRequest();
            var result = await _repo.UpdateAsync(currency);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// 刪除幣別資料。
        /// </summary>
        /// <param name="id">幣別主鍵</param>
        /// <returns>刪除成功回傳 204，否則 404</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
