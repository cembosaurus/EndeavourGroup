using Business.Inventory.DTOs.CatalogueProduct;
using Business.Inventory.DTOs.Product;
using Business.Inventory.DTOs.ProductPrice;
using Business.Inventory.Http.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using Trolley.HttpServices.Interfaces;



namespace Trolley.HttpServices
{
    public class HttpInventoryService : IHttpInventoryService
    {

        private readonly IHttpProductPriceClient _httpProductPriceClient;
        private readonly IHttpCatalogueProductClient _httpCatalogueProductClient;
        private readonly IHttpProductClient _httpProductClient;
        private readonly IServiceResultFactory _resutlFact;


        public HttpInventoryService(IHttpProductPriceClient httpProductPriceClient, IHttpCatalogueProductClient httpCatalogueProductClient, IHttpProductClient httpProductClient, IServiceResultFactory resutlFact)
        {
            _httpProductPriceClient = httpProductPriceClient;
            _httpCatalogueProductClient = httpCatalogueProductClient;
            _httpProductClient = httpProductClient;
            _resutlFact = resutlFact;
        }






        public async Task<IServiceResult<IEnumerable<ProductReadDTO>>> GetProducts(IEnumerable<int> productIds = default)
        {
            var response = await _httpProductClient.GetProducts(productIds);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result<IEnumerable<ProductReadDTO>>(null, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<ProductReadDTO>>>(content);

            return result;
        }




        public async Task<IServiceResult<ProductReadDTO>> GetProductById(int productId)
        {
            var response = await _httpProductClient.GetProductById(productId);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result<ProductReadDTO>(null, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<ProductReadDTO>>(content);

            return result;
        }




        public async Task<IServiceResult<IEnumerable<CatalogueProductReadDTO>>> GetCatalogueProducts(IEnumerable<int> productIds = default)
        {
            var response = await _httpCatalogueProductClient.GetCatalogueProducts(productIds);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result<IEnumerable<CatalogueProductReadDTO>>(null, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<CatalogueProductReadDTO>>>(content);

            return result;
        }




        public async Task<IServiceResult<CatalogueProductReadDTO>> GetCatalogueProductById(int productId)
        {
            var response = await _httpCatalogueProductClient.GetCatalogueProductById(productId);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result<CatalogueProductReadDTO>(null, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<CatalogueProductReadDTO>>(content);

            return result;
        }




        public async Task<IServiceResult<IEnumerable<ProductPriceReadDTO>>> GetProductPrices(IEnumerable<int> productIds = default)
        {
            var response = await _httpProductPriceClient.GetProductPrices(productIds);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result<IEnumerable<ProductPriceReadDTO>>(null, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<ProductPriceReadDTO>>>(content);

            return result;
        }




        public async Task<IServiceResult<ProductPriceReadDTO>> GetProductPriceById(int productId)
        {
            var response = await _httpProductPriceClient.GetProductPriceById(productId);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result<ProductPriceReadDTO>(null, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<ProductPriceReadDTO>>(content);

            return result;
        }




        public async Task<IServiceResult<int>> GetInstockCount(int productId)
        {
            var response =  await _httpCatalogueProductClient.GetInstockCount(productId);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result(0, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<int>>(content);

            return result;
        }




        public async Task<IServiceResult<int>> AddAmountToStock(int productId, int amount)
        {
            var response = await _httpCatalogueProductClient.AddAmountToStock(productId, amount);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result(0, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<int>>(content);

            return result;
        }




        public async Task<IServiceResult<int>> RemoveAmountFromStock(int productId, int amount)
        {
            var response = await _httpCatalogueProductClient.RemoveFromStockAmount(productId, amount);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result(0, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<int>>(content);

            return result;
        }



    }
}
