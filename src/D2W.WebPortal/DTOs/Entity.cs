using System.ComponentModel.DataAnnotations;

namespace D2W.WebPortal.DTOs
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
