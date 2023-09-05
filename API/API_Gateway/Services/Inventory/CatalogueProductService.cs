using API_Gateway.HttpServices.Inventory.Interfaces;
using API_Gateway.Services.Inventory.Interfaces;
using Business.Inventory.DTOs.CatalogueProduct;
using Business.Libraries.ServiceResult.Interfaces;



namespace API_Gateway.Services.Inventory
{
    public class CatalogueProductService : ICatalogueProductService
    {

        private readonly IHttpCatalogueProductService _httpCatalogueProductService;


        public CatalogueProductService(IHttpCatalogueProductService httpCatalogueProductService)
        {
            _httpCatalogueProductService = httpCatalogueProductService;
        }






        public async Task<IServiceResult<int>> AddAmountToStock(int productId, int amount)
        {
            return await _httpCatalogueProductService.AddAmountToStock(productId, amount);
        }



        public async Task<IServiceResult<CatalogueProductReadDTO>> CreateCatalogueProduct(int productId, CatalogueProductCreateDTO catalogueProductCreateDTO)
        {
            return await _httpCatalogueProductService.CreateCatalogueProduct(productId, catalogueProductCreateDTO);
        }



        public async Task<IServiceResult<bool>> ExistsCatalogueProductById(int id)
        {
            return await _httpCatalogueProductService.ExistsCatalogueProductById(id);
        }



        public async Task<IServiceResult<CatalogueProductReadDTO>> GetCatalogueProductById(int id)
        {
            return await _httpCatalogueProductService.GetCatalogueProductById(id);
        }



        public async Task<IServiceResult<IEnumerable<CatalogueProductReadDTO>>> GetCatalogueProducts(IEnumerable<int> productIds = null)
        {
            return await _httpCatalogueProductService.GetCatalogueProducts(productIds);
        }



        public async Task<IServiceResult<int>> GetInstockCount(int id)
        {
            return await _httpCatalogueProductService.GetInstockCount(id);
        }



        public async Task<IServiceResult<CatalogueProductReadDTO>> RemoveCatalogueProduct(int id)
        {
            return await _httpCatalogueProductService.RemoveCatalogueProduct(id);
        }




        public async Task<IServiceResult<int>> RemoveFromStockAmount(int productId, int amount)
        {
            return await _httpCatalogueProductService.RemoveFromStockAmount(productId, amount);
        }



        public async Task<IServiceResult<CatalogueProductReadDTO>> UpdateCatalogueProduct(int productId, CatalogueProductUpdateDTO catalogueProductUpdateDTO)
        {
            return await _httpCatalogueProductService.UpdateCatalogueProduct(productId, catalogueProductUpdateDTO);
        }
    }
}
