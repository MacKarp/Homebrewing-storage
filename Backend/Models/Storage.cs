using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Storage
    {
        [Key]
        public int StorageId { get; set; }
        [ForeignKey("UserId")]
        public virtual User IdUser { get; set; }
        [Required]
        public string StorageName { get; set; }
    }
}