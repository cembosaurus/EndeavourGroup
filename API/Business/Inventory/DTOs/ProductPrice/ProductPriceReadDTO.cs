namespace Business.Inventory.DTOs.ProductPrice
{
    public class ProductPriceReadDTO
    {
        public int ProductId { get; set; }
        public double SalePrice { get; set; }
        public double? RRP { get; set; }
        public int? DiscountPercent { get; set; }

        public double DiscountedPrice 
        { 
            get { 
                return DiscountPercent > 0 ? Math.Round(SalePrice - (SalePrice / 100 * DiscountPercent).Value, 2) : SalePrice; 
            } 
        }
    }
}
