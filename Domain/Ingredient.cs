namespace Domain
{
    public class Ingredient
    {
        public int Id { get; set; }

        public Item Item { get; set; }

        public decimal ItemsPerMinute { get; set; }
    }
}