using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos
{
    public class ItemUpdateDto
    {
        [Required]
        public string ItemName { get; set; }
        [Required]
        public virtual Category NamCategory { get; set; } //ustawić mapowanie na int zamiast zwracać cały obiekt
    }
}