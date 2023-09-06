namespace Business.Inventory.DTOs.ProductPrice
{
    public class ProductPriceReadDTO
    {
        public int ProductId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? RRP { get; set; }
        public int? DiscountPercent { get; set; }

        public decimal Discount
        {
            get
            {
                return DiscountPercent > 0 ? Math.Round((SalePrice / 100 * DiscountPercent).Value, 2) : 0;
            }
        }
        public decimal DiscountedPrice 
        { 
            get { 
                return DiscountPercent > 0 ? Math.Round(SalePrice - (SalePrice / 100 * DiscountPercent).Value, 2) : SalePrice; 
            } 
        }
    }
}
