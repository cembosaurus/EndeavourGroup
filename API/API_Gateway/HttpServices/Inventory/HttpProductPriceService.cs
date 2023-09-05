using API_Gateway.HttpServices.Inventory.Interfaces;
using Business.Inventory.DTOs.ProductPrice;
using Business.Inventory.Http.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;



namespace API_Gateway.HttpServices.Inventory
{
    public class HttpProductPriceService : IHttpProductPriceService
    {

        private readonly IHttpProductPriceClient _httpProductPriceClient;
        private readonly IServiceResultFactory _resultFact;


        public HttpProductPriceService(IHttpProductPriceClient httpProductPriceClient, IServiceResultFactory resultFact)
        {
            _httpProductPriceClient = httpProductPriceClient;
            _resultFact = resultFact;
        }





        public async Task<IServiceResult<ProductPriceReadDTO>> GetProductPriceById(int id)
        {
            var response = await _httpProductPriceClient.GetProductPriceById(id);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<ProductPriceReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<ProductPriceReadDTO>>(content);

            return result;
        }



        public async Task<IServiceResult<IEnumerable<ProductPriceReadDTO>>> GetProductPrices(IEnumerable<int> productIds = default)
        {
            var response = await _httpProductPriceClient.GetProductPrices(productIds);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<ProductPriceReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<ProductPriceReadDTO>>>(content);

            return result;
        }



        public async Task<IServiceResult<ProductPriceReadDTO>> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceEditDTO)
        {
            var response = await _httpProductPriceClient.UpdateProductPrice(productId, productPriceEditDTO);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<ProductPriceReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<ProductPriceReadDTO>>(content);

            return result;
        }
    }
}
