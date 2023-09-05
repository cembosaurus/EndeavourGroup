using API_Gateway.HttpServices.Trolley.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Business.Trolley.Http.Interfaces;



namespace API_Gateway.HttpServices.Trolley
{
    public class HttpTrolleyProductService : IHttpTrolleyProductService
    {

        private readonly IHttpTrolleyProductClient _httpTrolleyProductClient;
        private readonly IServiceResultFactory _resultFact;


        public HttpTrolleyProductService(IHttpTrolleyProductClient httpTrolleyProductClient, IServiceResultFactory resultFact)
        {
            _httpTrolleyProductClient = httpTrolleyProductClient;
            _resultFact = resultFact;
        }






        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> AddProductsToTrolley(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToAdd)
        {
            var response = await _httpTrolleyProductClient.AddProductsToTrolley(trolleyId, productsToAdd);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyProductReadDTO>>>(content);

            return result;
        }



        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> DeleteTrolleyProducts(int userId, IEnumerable<int> productIds)
        {
            var response = await _httpTrolleyProductClient.DeleteTrolleyProducts(userId, productIds);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyProductReadDTO>>>(content);

            return result;
        }



        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetAllTrolleyProducts()
        {
            var response = await _httpTrolleyProductClient.GetAllTrolleyProducts();

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyProductReadDTO>>>(content);

            return result;
        }



        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetTrolleyProducts(int userId)
        {
            var response = await _httpTrolleyProductClient.GetTrolleyProducts(userId);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyProductReadDTO>>>(content);

            return result;
        }



        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> RemoveTrolleyProducts(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> productsToRemove)
        {
            var response = await _httpTrolleyProductClient.RemoveTrolleyProducts(trolleyId, productsToRemove);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyProductReadDTO>>>(content);

            return result;
        }
    }
}
