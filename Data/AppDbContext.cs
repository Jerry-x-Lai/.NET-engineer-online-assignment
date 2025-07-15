using Microsoft.EntityFrameworkCore;
using CryptoInfoApi.Models;

namespace CryptoInfoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Currency> Currencies { get; set; }
    }
}
