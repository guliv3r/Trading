using Trading.Infrastructure.Database.Enum;

namespace Trading.Core.Models.Request
{
    public class ChangePriceRequest
    {
        public int CompanyId { get; set; }
        public int MarketId { get; set; }
        public int Price { get; set; }
        public CcyEnum Ccy { get; set; }
    }
}
