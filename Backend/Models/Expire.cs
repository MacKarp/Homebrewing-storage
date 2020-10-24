namespace Backend.Models
{
    public class Expire
    {
        public int Id { get; set; }
        public int UserId { get; set; } //temporary, need to change to proper UserID
        public virtual Storage IdStorage { get; set; }
        public virtual Item IdItem { get; set; }
        public string ExpirationDate { get; set; } //need to change to proper Data format
    }
}