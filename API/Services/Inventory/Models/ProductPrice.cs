namespace Services.Inventory.Models
{
    public class ProductPrice
    {
        public int ProductId { get; set; }
        public double SalePrice { get; set; }
        public double? RRP { get; set; }
        public int? DiscountPercent { get; set; }

        public CatalogueProduct CatalogueProduct { get; set; }
    }
}
