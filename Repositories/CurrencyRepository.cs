using CryptoInfoApi.Data;
using CryptoInfoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoInfoApi.Repositories
{
    /// <summary>
    /// 幣別資料存取實作，提供 CRUD 操作。
    /// </summary>
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;
        
        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 取得所有幣別資料。
        /// </summary>
        /// <returns>幣別清單</returns>
        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _context.Currencies.OrderBy(c => c.Code).ToListAsync();
        }

        /// <summary>
        /// 依據 Id 取得幣別資料。
        /// </summary>
        /// <param name="id">幣別主鍵</param>
        /// <returns>幣別資料或 null</returns>
        public async Task<Currency?> GetByIdAsync(int id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        /// <summary>
        /// 依據幣別代碼取得資料。
        /// </summary>
        /// <param name="code">幣別代碼</param>
        /// <returns>幣別資料或 null</returns>
        public async Task<Currency?> GetByCodeAsync(string code)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Code == code);
        }

        /// <summary>
        /// 新增幣別資料。
        /// </summary>
        /// <param name="currency">幣別物件</param>
        /// <returns>新增後的幣別資料</returns>
        public async Task<Currency> AddAsync(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();
            return currency;
        }

        /// <summary>
        /// 更新幣別資料。
        /// </summary>
        /// <param name="currency">幣別物件</param>
        /// <returns>更新後的幣別資料或 null</returns>
        public async Task<Currency?> UpdateAsync(Currency currency)
        {
            var existing = await _context.Currencies.FindAsync(currency.Id);
            if (existing == null) return null;
            existing.Code = currency.Code;
            existing.ChineseName = currency.ChineseName;
            await _context.SaveChangesAsync();
            return existing;
        }

        /// <summary>
        /// 刪除幣別資料。
        /// </summary>
        /// <param name="id">幣別主鍵</param>
        /// <returns>刪除成功與否</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null) return false;
            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
