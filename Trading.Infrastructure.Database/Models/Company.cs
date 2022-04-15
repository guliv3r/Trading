namespace Trading.Infrastructure.Database.Models
{
    public class Company : BaseEntity<int>
    {
        public string IdentityCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }
}
