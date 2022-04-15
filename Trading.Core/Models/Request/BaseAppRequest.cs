namespace Trading.Core.Models.Request
{
    public class BaseAppRequest
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
