using Trading.Infrastructure.Database.Enum;

namespace Trading.Core.Models
{
    public class TradingDto
    {
        public int Id { get; set; }
        public CompanyDto Company { get; set; }
        public MarketDto Market { get; set; }
        public int Price { get; set; }
        public CcyEnum Ccy { get; set; }
        public TradingStatusEnum Status { get; set; }
    }
}
