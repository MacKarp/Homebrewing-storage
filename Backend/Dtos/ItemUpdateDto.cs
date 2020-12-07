using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos
{
    public class ItemUpdateDto
    {
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}