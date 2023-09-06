namespace Services.Inventory.Models
{
    public class ProductPrice
    {
        public int ProductId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? RRP { get; set; }
        public int? DiscountPercent { get; set; }

        public CatalogueProduct CatalogueProduct { get; set; }
    }
}
