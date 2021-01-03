using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Storage
    {
        [Key]
        public int StorageId { get; set; }
        [ForeignKey("Id")]
        public virtual IdentityUser IdUser { get; set; }
        [Required]
        public string StorageName { get; set; }
    }
}