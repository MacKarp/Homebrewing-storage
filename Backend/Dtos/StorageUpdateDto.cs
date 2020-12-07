using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class StorageUpdateDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string StorageName { get; set; }
    }
}