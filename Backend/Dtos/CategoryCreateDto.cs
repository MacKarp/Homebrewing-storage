using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class CategoryCreateDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}