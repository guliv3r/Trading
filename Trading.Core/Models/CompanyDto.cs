namespace Trading.Core.Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string IdentityCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
