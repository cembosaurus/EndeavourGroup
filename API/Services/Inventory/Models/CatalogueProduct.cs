namespace Services.Inventory.Models
{
    public record CatalogueProduct
    {
        public int ProductId { get; set; }
        public string? Description { get; set; }
        public int Instock { get; set; }

        public ProductPrice ProductPrice { get; set; }
        public Product Product { get; set; }
    }
}
