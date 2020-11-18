using Backend.Models;

namespace Backend.Dtos
{
    public class ItemCreateDto
    {
        public string ItemName { get; set; }
        public virtual Category NamCategory { get; set; } //ustawić mapowanie na int zamiast zwracać cały obiekt
    }
}