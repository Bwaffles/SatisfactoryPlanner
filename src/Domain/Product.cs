namespace Domain
{
    public class Product
    {
        public int Id { get; set; }

        public Item Item { get; set; }

        public decimal ItemsPerMinute { get; set; }
    }
}