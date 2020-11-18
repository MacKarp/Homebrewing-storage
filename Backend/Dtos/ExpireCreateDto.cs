using Backend.Models;

namespace Backend.Dtos
{
    public class ExpireCreateDto
    {
        public int UserId { get; set; } //temporary, need to change to proper UserID
        public virtual Storage IdStorage { get; set; } //ustawić mapowanie na int zamiast zwracać cały obiekt 
        public virtual Item IdItem { get; set; } //ustawić mapowanie na int zamiast zwracać cały obiekt 
        public string ExpirationDate { get; set; } //need to change to proper Data format
    }
}