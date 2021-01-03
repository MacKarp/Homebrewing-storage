using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Expire
    {
        [Key]
        public int ExpireId { get; set; }
        [ForeignKey("Id")]
        public virtual IdentityUser IdUser { get; set; }
        [ForeignKey("StorageId")]
        public virtual Storage IdStorage { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item IdItem { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime ExpirationDate { get; set; }
    }
}