using Business.Inventory.DTOs.CatalogueProduct;
using Business.Libraries.ServiceResult.Interfaces;



namespace API_Gateway.Services.Inventory.Interfaces
{
    public interface ICatalogueProductService
    {
        Task<IServiceResult<int>> AddAmountToStock(int productId, int amount);
        Task<IServiceResult<CatalogueProductReadDTO>> CreateCatalogueProduct(int productId, CatalogueProductCreateDTO catalogueProductCreateDTO);
        Task<IServiceResult<bool>> ExistsCatalogueProductById(int id);
        Task<IServiceResult<CatalogueProductReadDTO>> GetCatalogueProductById(int id);
        Task<IServiceResult<IEnumerable<CatalogueProductReadDTO>>> GetCatalogueProducts(IEnumerable<int> productIds = null);
        Task<IServiceResult<int>> GetInstockCount(int id);
        Task<IServiceResult<CatalogueProductReadDTO>> RemoveCatalogueProduct(int id);
        Task<IServiceResult<int>> RemoveFromStockAmount(int productId, int amount);
        Task<IServiceResult<CatalogueProductReadDTO>> UpdateCatalogueProduct(int productId, CatalogueProductUpdateDTO catalogueProductUpdateDTO);
    }
}
