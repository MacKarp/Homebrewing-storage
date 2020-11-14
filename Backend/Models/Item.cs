using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public virtual Category IdCategory { get; set; }
    }
}