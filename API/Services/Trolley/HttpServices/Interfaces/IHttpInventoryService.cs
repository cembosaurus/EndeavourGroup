using Business.Inventory.DTOs.CatalogueProduct;
using Business.Inventory.DTOs.Product;
using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult.Interfaces;



namespace Trolley.HttpServices.Interfaces
{
    public interface IHttpInventoryService
    {
        Task<IServiceResult<int>> AddAmountToStock(int productId, int amount);
        Task<IServiceResult<CatalogueProductReadDTO>> GetCatalogueProductById(int productId);
        Task<IServiceResult<IEnumerable<CatalogueProductReadDTO>>> GetCatalogueProducts(IEnumerable<int> productIds = null);
        Task<IServiceResult<int>> GetInstockCount(int productId);
        Task<IServiceResult<ProductReadDTO>> GetProductById(int productId);
        Task<IServiceResult<ProductPriceReadDTO>> GetProductPriceById(int productId);
        Task<IServiceResult<IEnumerable<ProductPriceReadDTO>>> GetProductPrices(IEnumerable<int> productIds = null);
        Task<IServiceResult<IEnumerable<ProductReadDTO>>> GetProducts(IEnumerable<int> productIds = null);
        Task<IServiceResult<int>> RemoveAmountFromStock(int productId, int amount);
    }
}
