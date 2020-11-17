using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Expire
    {
        [Key]
        public int ExpireId { get; set; }
        [Required]
        public int UserId { get; set; } //temporary, need to change to proper UserID
        [ForeignKey("StorageId")]
        public virtual Storage IdStorage { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item IdItem { get; set; }
        [Required]
        public string ExpirationDate { get; set; } //need to change to proper Data format
    }
}