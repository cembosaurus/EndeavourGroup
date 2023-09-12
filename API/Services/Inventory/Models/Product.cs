namespace Services.Inventory.Models
{
    public record Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public string? PhotoURL { get; set; }
        public bool Archived { get; set; }


        public CatalogueProduct CatalogueProduct { get; set; }
    }
}
