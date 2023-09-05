using Business.Inventory.DTOs.Product;
using Business.Inventory.DTOs.ProductPrice;

namespace Business.Inventory.DTOs.CatalogueProduct
{
    public class CatalogueProductReadDTOFull
    {
        public int ProductId { get; set; }
        public string? Description { get; set; }
        public ProductReadDTO Product { get; set; }
        public ProductPriceReadDTO ProductPrice { get; set; }
        public int Instock { get; set; }

    }
}
