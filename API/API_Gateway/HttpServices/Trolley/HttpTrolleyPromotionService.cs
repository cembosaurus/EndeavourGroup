using API_Gateway.HttpServices.Trolley.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Business.Trolley.Http.Interfaces;

namespace API_Gateway.HttpServices.Trolley
{
    public class HttpTrolleyPromotionService : IHttpTrolleyPromotionService
    {

        private readonly IHttpTrolleyPromotionClient _httpTrolleyPromotionClient;
        private readonly IServiceResultFactory _resultFact;


        public HttpTrolleyPromotionService(IHttpTrolleyPromotionClient httpTrolleyPromotionClient, IServiceResultFactory resultFact)
        {
            _httpTrolleyPromotionClient = httpTrolleyPromotionClient;
            _resultFact = resultFact;
        }








        public async Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetAllTrolleyPromotions()
        {
            var response = await _httpTrolleyPromotionClient.GetAllTrolleyPromotions();

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyPromotionReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage?.Method}, {response.RequestMessage?.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyPromotionReadDTO>>>(content);

            return result;
        }
          


        public async Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetActiveTrolleyPromotions()
        {
            var response = await _httpTrolleyPromotionClient.GetActiveTrolleyPromotions();

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyPromotionReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage?.Method}, {response.RequestMessage?.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyPromotionReadDTO>>>(content);

            return result;
        }
    }
}
