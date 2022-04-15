using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trading.Infrastructure.Database.Models
{
    public abstract class BaseEntity<TID>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TID Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
