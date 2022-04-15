using Trading.Core.Models.Request;
using Trading.Infrastructure.Database.Models;

namespace Trading.Core.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> Add(Company company);
        Task<Company> Update(Company company);
        Task<bool> Remove(int id);
        Task<Company> GetById(int id);
        Task<List<Company>> Get();
        Task ChangePrice(ChangePriceRequest changePriceRequest);
    }
}
