using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}