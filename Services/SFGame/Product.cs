namespace Services.SFGame
{
    public class Product
    {
        public string ItemClass { get; set; }
        public decimal Amount { get; set; } // TODO unsure if decimal or int
        public Item Item { get; set; }
    }
}
