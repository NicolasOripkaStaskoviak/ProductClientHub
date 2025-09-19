namespace ProductClientHub.API.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public int Price { get; set; }

        public Guid ClientId { get; set; }
    }
}
