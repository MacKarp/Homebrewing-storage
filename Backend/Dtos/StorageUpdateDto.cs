using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class StorageUpdateDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string StorageName { get; set; }
    }
}