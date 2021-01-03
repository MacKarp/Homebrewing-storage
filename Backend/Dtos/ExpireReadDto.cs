using Backend.Models;

namespace Backend.Dtos
{
    public class ExpireReadDto
    {
        public int ExpireId { get; set; }
        public string IdUser { get; set; }
        public int IdStorage { get; set; }
        public int IdItem { get; set; }
        public string ExpirationDate { get; set; }
    }
}