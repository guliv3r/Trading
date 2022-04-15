using Trading.Core.Models;
using Trading.Infrastructure.Database.Models;

namespace Trading.Core.Mapper
{
    public static class MapperToEntity
    {
        public static Market? ToEntity(this MarketDto market)
        {
            return new Market()
            {
                Description = market.Description,
                Name = market.Name
            };
        }

        public static Company ToEntity(this CompanyDto company)
        {
            return new Company()
            {
                Description = company.Description,
                IdentityCode = company.IdentityCode,
                Logo = company.Logo,
                Name = company.Name,
                Id = company.Id,
                CreateDate = company.CreateDate
            };
        }
    }
}
