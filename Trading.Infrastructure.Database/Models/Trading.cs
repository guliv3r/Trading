using Trading.Infrastructure.Database.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trading.Infrastructure.Database.Models
{
    public class Trading : BaseEntity<int>
    {
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [ForeignKey("MarketId")]
        public int MarketId { get; set; }
        public virtual Market Market { get; set; }
        public int Price { get; set; }
        public CcyEnum Ccy { get; set; }
        public TradingStatusEnum Status { get; set; }
    }
}
