using Trading.Infrastructure.Database.Models;

namespace Trading.Core.Repositories
{
    public interface IMarketRepository
    {
        Task<Market> Add(Market market);
        Task<Market> Update(Market market);
        Task<bool> Remove(int id);
        Task<Market> GetById(int id);
        Task<List<Market>> Get();
        Task<List<Infrastructure.Database.Models.Trading>> GetMarketCompanies(int marketId);
        Task<Infrastructure.Database.Models.Trading> GetMarketCompanyById(int companyId);
    }
}
