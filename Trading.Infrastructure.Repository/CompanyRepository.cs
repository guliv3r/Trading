using Microsoft.EntityFrameworkCore;
using Trading.Core.Models.Request;
using Trading.Core.Repositories;
using Trading.Infrastructure.Database;
using Trading.Infrastructure.Database.Enum;
using Trading.Infrastructure.Database.Models;

namespace Trading.Infrastructure.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TradingDbContext _dbContext;

        public CompanyRepository(TradingDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Company> Add(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return company;
        }

        public async Task<Company> Update(Company company)
        {
            var res = await _dbContext.Companies.SingleAsync(x => x.Id == company.Id);
            res.Description = company.Description;
            res.Name = company.Name;
            res.IdentityCode = company.IdentityCode;
            res.Logo = company.Logo;
            await _dbContext.SaveChangesAsync();
            return company;
        }

        public async Task<bool> Remove(int id)
        {
            var res = await _dbContext.Companies.SingleAsync(x => x.Id == id);
            _dbContext.Companies.Remove(res);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Company> GetById(int id)
            => await _dbContext.Companies.SingleAsync(_ => _.Id == id);

        public async Task<List<Company>> Get()
            => await _dbContext.Companies.OrderByDescending(x => x.CreateDate).ToListAsync();

        public async Task ChangePrice(ChangePriceRequest changePriceRequest)
        {
            var res = _dbContext.Tradings.Where(x => x.MarketId == changePriceRequest.MarketId && x.CompanyId == changePriceRequest.CompanyId && x.Status == TradingStatusEnum.Active);
            if (res.Any()) await res.ForEachAsync(x => x.Status = TradingStatusEnum.Archived);
            await _dbContext.Tradings.AddAsync(new Database.Models.Trading()
            {
                Ccy = changePriceRequest.Ccy,
                CompanyId = changePriceRequest.CompanyId,
                MarketId = changePriceRequest.MarketId,
                Status = TradingStatusEnum.Active,
                Price = changePriceRequest.Price
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}