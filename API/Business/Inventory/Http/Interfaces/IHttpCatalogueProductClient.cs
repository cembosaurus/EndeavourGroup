using Business.Inventory.DTOs.CatalogueProduct;



namespace Business.Inventory.Http.Interfaces
{
    public interface IHttpCatalogueProductClient
    {
        Task<HttpResponseMessage> AddAmountToStock(int productId, int amount);
        Task<HttpResponseMessage> CreateCatalogueProduct(int productId, CatalogueProductCreateDTO catalogueProductCreateDTO);
        Task<HttpResponseMessage> ExistsCatalogueProductById(int productId);
        Task<HttpResponseMessage> GetCatalogueProductById(int productId);
        Task<HttpResponseMessage> GetCatalogueProducts(IEnumerable<int> productIds = null);
        Task<HttpResponseMessage> GetInstockCount(int productId);
        Task<HttpResponseMessage> RemoveCatalogueProduct(int productId);
        Task<HttpResponseMessage> RemoveFromStockAmount(int productId, int amount);
        Task<HttpResponseMessage> UpdateCatalogueProduct(int productId, CatalogueProductUpdateDTO catalogueProductUpdateDTO);
    }
}
