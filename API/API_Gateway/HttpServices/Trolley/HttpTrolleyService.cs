using API_Gateway.HttpServices.Trolley.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Business.Trolley.Http.Interfaces;



namespace API_Gateway.HttpServices.Trolley
{
    public class HttpTrolleyService : IHttpTrolleyService
    {

        private readonly IHttpTrolleyClient _httpTrolleyClient;
        private readonly IServiceResultFactory _resultFact;


        public HttpTrolleyService(IHttpTrolleyClient httpTrolleyClient, IServiceResultFactory resultFact)
        {
            _httpTrolleyClient = httpTrolleyClient;
            _resultFact = resultFact;
        }





        public async Task<IServiceResult<TrolleyReadDTO>> CreateTrolley(int userId)
        {
            var response = await _httpTrolleyClient.CreateTrolley(userId);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<TrolleyReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<TrolleyReadDTO>>(content);

            return result;
        }



        public async Task<IServiceResult<TrolleyReadDTO>> DeleteTrolley(int id)
        {
            var response = await _httpTrolleyClient.DeleteTrolley(id);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<TrolleyReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<TrolleyReadDTO>>(content);

            return result;
        }



        public async Task<IServiceResult<bool>> ExistsTrolleyByTrolleyId(Guid trolleyId)
        {
            var response = await _httpTrolleyClient.ExistsTrolleyByTrolleyId(trolleyId);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result(false, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<bool>>(content);

            return result;
        }



        public async Task<IServiceResult<IEnumerable<TrolleyReadDTO>>> GetTrolleys()
        {
            var response = await _httpTrolleyClient.GetTrolleys();

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<IEnumerable<TrolleyReadDTO>>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<IEnumerable<TrolleyReadDTO>>>(content);

            return result;
        }



        public async Task<IServiceResult<TrolleyReadDTO>> GetTrolleyByUserId(int userId)
        {
            var response = await _httpTrolleyClient.GetTrolleyByUserId(userId);

            if (!response.IsSuccessStatusCode)
                return _resultFact.Result<TrolleyReadDTO>(null, false, $"{response.ReasonPhrase}: {response.RequestMessage.Method}, {response.RequestMessage.RequestUri}");

            var content = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceResult<TrolleyReadDTO>>(content);

            return result;
        }


    }
}
