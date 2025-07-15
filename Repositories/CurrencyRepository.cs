using CryptoInfoApi.Data;
using CryptoInfoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoInfoApi.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;
        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _context.Currencies.OrderBy(c => c.Code).ToListAsync();
        }

        public async Task<Currency?> GetByIdAsync(int id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public async Task<Currency?> GetByCodeAsync(string code)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<Currency> AddAsync(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();
            return currency;
        }

        public async Task<Currency?> UpdateAsync(Currency currency)
        {
            var existing = await _context.Currencies.FindAsync(currency.Id);
            if (existing == null) return null;
            existing.Code = currency.Code;
            existing.ChineseName = currency.ChineseName;
            await _context.SaveChangesAsync();
            return existing;
        }

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
