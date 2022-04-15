using Trading.Core.Models;
using Trading.Infrastructure.Database.Models;

namespace Trading.Core.Mapper
{
    public static class MapperToDomain
    {
        public static CompanyDto? ToDomain(this Company company)
        {
            if (company == null) return null;
            return new CompanyDto()
            {
                Description = company.Description,
                IdentityCode = company.IdentityCode,
                Logo = company.Logo,
                Name = company.Name,
                Id = company.Id,
                CreateDate = company.CreateDate,
            };
        }

        public static MarketDto? ToDomain(this Market market)
        {
            if (market == null) return null;
            return new MarketDto()
            {
                Description= market.Description,
                Id = market.Id,
                Name = market.Name,
                CreateDate = market.CreateDate
            };
        }

        public static TradingDto ToDomain(this Trading.Infrastructure.Database.Models.Trading trading)
        {
            if (trading == null) return null;
            return new TradingDto()
            {
                Id = trading.Id,
                Ccy = trading.Ccy,
                Price = trading.Price,
                Status = trading.Status,
                Company = trading.Company?.ToDomain(),
                Market = trading.Market?.ToDomain(),
            };
        }
    }
}
