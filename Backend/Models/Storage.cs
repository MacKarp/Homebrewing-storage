using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserID { get; set; } //temporary, need to change to proper UserID
        [Required]
        public string StorageName { get; set; }
    }
}