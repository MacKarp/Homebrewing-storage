using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos
{
    public class ExpireCreateDto
    {
        [Required]
        public int UserId { get; set; } //temporary, need to change to proper UserID
        [Required]
        public virtual Storage IdStorage { get; set; } //ustawić mapowanie na int zamiast zwracać cały obiekt 
        [Required]
        public virtual Item IdItem { get; set; } //ustawić mapowanie na int zamiast zwracać cały obiekt 
        [Required]
        public string ExpirationDate { get; set; } //need to change to proper Data format
    }
}