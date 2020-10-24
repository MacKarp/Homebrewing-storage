namespace Backend.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int UserID { get; set; } //temporary, need to change to proper UserID
        public string StorageName { get; set; }
    }
}