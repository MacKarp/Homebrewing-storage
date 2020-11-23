using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class StorageCreateDto
    {
        [Required]
        public int UserID { get; set; } //temporary, need to change to proper UserID
        [Required]
        public string StorageName { get; set; }
    }
}