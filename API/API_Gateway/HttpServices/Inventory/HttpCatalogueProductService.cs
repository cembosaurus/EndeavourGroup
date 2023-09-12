using API_Gateway.HttpServices.Inventory.Interfaces;
using Business.Inventory.DTOs.CatalogueProduct;
using Business.Inventory.Http.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;



namespace API_Gateway.HttpServices.Inventory
{
    public class HttpCatalogueProductService : IHttpCatalogueProductService
    {

        private readonly IHttpCatalogueProductClient _httpCatalogueProductClient;
        private readonly IServiceResultFactory _resultFact;


        public HttpCatalogueProductService(IHttpCatalogueProductClient httpCatalogueProductClient, IServiceResultFactory resultFact)
        {
            _httpCatalogueProductClient = httpCatalogueProductClient;
            _resultFact = resultFact;
        }





        public async Task<IServiceResult<int>> AddAmountToStock(int productId, int amount)
        {
            var response = await _httpCatalogueProductClient.AddAmountToStock(productId, amount);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result(0, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<int>>(content);

            return result;
        }




        public async Task<IServiceResult<CatalogueProductReadDTO>> CreateCatalogueProduct(int productId, CatalogueProductCreateDTO catalogueProductCreateDTO)
        {
            var response = await _httpCatalogueProductClient.CreateCatalogueProduct(productId, catalogueProductCreateDTO);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<CatalogueProductReadDTO>>(content);

            return result;
        }



        public async Task<IServiceResult<bool>> ExistsCatalogueProductById(int id)
        {
            var response = await _httpCatalogueProductClient.ExistsCatalogueProductById(id);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result(false, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<bool>>(content);

            return result;
        }





        public async Task<IServiceResult<CatalogueProductReadDTO>> GetCatalogueProductById(int id)
        {
            var response = await _httpCatalogueProductClient.GetCatalogueProductById(id);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<CatalogueProductReadDTO>>(content);

            return result;
        }




        public async Task<IServiceResult<IEnumerable<CatalogueProductReadDTO>>> GetCatalogueProducts(IEnumerable<int> productIds = null)
        {
            var response = await _httpCatalogueProductClient.GetCatalogueProducts(productIds);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<CatalogueProductReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<CatalogueProductReadDTO>>>(content);

            return result;
        }





        public async Task<IServiceResult<int>> GetInstockCount(int id)
        {
            var response = await _httpCatalogueProductClient.GetInstockCount(id);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result(0, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<int>>(content);

            return result;
        }





        public async Task<IServiceResult<CatalogueProductReadDTO>> RemoveCatalogueProduct(int id)
        {
            var response = await _httpCatalogueProductClient.RemoveCatalogueProduct(id);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<CatalogueProductReadDTO>>(content);

            return result;
        }




        public async Task<IServiceResult<int>> RemoveFromStockAmount(int productId, int amount)
        {
            var response = await _httpCatalogueProductClient.RemoveFromStockAmount(productId, amount);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result(0, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<int>>(content);

            return result;
        }




        public async Task<IServiceResult<CatalogueProductReadDTO>> UpdateCatalogueProduct(int productId, CatalogueProductUpdateDTO catalogueProductUpdateDTO)
        {
            var response = await _httpCatalogueProductClient.UpdateCatalogueProduct(productId, catalogueProductUpdateDTO);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<CatalogueProductReadDTO>>(content);

            return result;
        }
    }
}
