namespace Trading.Infrastructure.Database.Models
{
    public class Market : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
