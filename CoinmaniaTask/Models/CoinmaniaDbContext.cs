using Microsoft.EntityFrameworkCore;

namespace CoinmaniaTask.Models
{
    public class CoinmaniaDbContext : DbContext
    {
        public CoinmaniaDbContext(DbContextOptions<CoinmaniaDbContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyHistory> CurrencyHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasKey(c => c.Base);
        }
    }
}
