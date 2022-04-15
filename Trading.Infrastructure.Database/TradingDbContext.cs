using Trading.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Trading.Infrastructure.Database
{
    public class TradingDbContext : DbContext
    {
        public TradingDbContext(DbContextOptions<TradingDbContext> dbContextOptions) : base(dbContextOptions){ }

        public DbSet<Market> Markets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Models.Trading> Tradings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>().HasIndex(p => p.IdentityCode).IsUnique();

            //builder.Entity<Models.Trading>()
            //    .HasIndex(p => new { p.MarketId, p.CompanyId, p.Status })
            //    .IsUnique();
        }
    }
}