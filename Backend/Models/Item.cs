namespace Backend.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public virtual Category NamCategory { get; set; }
    }
}