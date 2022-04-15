using Microsoft.EntityFrameworkCore;
using Trading.Core.Repositories;
using Trading.Infrastructure.Database;
using Trading.Infrastructure.Database.Enum;
using Trading.Infrastructure.Database.Models;

namespace Trading.Infrastructure.Repository
{
    public class MarketRepository : IMarketRepository
    {
        private readonly TradingDbContext _dbContext;

        public MarketRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Market> Add(Market market)
        {
            await _dbContext.Markets.AddAsync(market);
            await _dbContext.SaveChangesAsync();
            return market;
        }

        public async Task<Market> Update(Market market)
        {
            var res = await _dbContext.Markets.SingleAsync(x => x.Id == market.Id);
            res.Description = market.Description;
            res.Name = market.Name;
            await _dbContext.SaveChangesAsync();
            return market;
        }

        public async Task<bool> Remove(int id)
        {
            var res = await _dbContext.Markets.SingleAsync(x => x.Id == id);
            _dbContext.Markets.Remove(res);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Market> GetById(int id)
            => await _dbContext.Markets.SingleAsync(_ => _.Id == id);

        public async Task<List<Market>> Get()
            => await _dbContext.Markets.OrderByDescending(x => x.CreateDate).ToListAsync();

        public async Task<List<Database.Models.Trading>> GetMarketCompanies(int marketId)
            => await _dbContext.Tradings.Include(x => x.Market).Include(x => x.Company).Where(x => x.MarketId == marketId && x.Status == TradingStatusEnum.Active).ToListAsync();

        public async Task<Database.Models.Trading> GetMarketCompanyById(int companyId)
            => await _dbContext.Tradings.Include(x => x.Market).Include(x => x.Company).Where(x => x.CompanyId == companyId && x.Status == TradingStatusEnum.Active).SingleAsync();
    }
}
