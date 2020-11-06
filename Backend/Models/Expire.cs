using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Expire
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; } //temporary, need to change to proper UserID
        [Required]
        public virtual Storage IdStorage { get; set; }
        [Required]
        public virtual Item IdItem { get; set; }
        [Required]
        public string ExpirationDate { get; set; } //need to change to proper Data format
    }
}