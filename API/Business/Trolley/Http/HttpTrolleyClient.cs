using Business.Trolley.Http.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;



namespace Business.Trolley.Http
{
    public class HttpTrolleyClient : IHttpTrolleyClient
    {

        private HttpRequestMessage _request;
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _mediaType = "application/json";


        public HttpTrolleyClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = config.GetSection("RemoteServices:TrolleyService").Value + "/api/trolley";
        }






        public async Task<HttpResponseMessage> CreateTrolley(int userId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Post,
                $"/{userId}"
            );

            Console.WriteLine($"---> CREATING trolley for user '{userId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> ExistsTrolleyByTrolleyId(Guid trolleyId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/exists",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new { trolleyId }), _encoding, _mediaType)
            );

            Console.WriteLine($"---> EXISTS trolley '{trolleyId}' ....");

            return await _httpClient.SendAsync(_request);
        }




        public async Task<HttpResponseMessage> DeleteTrolley(int id)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Delete,
                $"/{id}"
            );

            Console.WriteLine($"---> DELETING trolley ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetTrolleys()
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/all"
            );

            Console.WriteLine($"---> GETTING trolleys ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetTrolleyByUserId(int userId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/{userId}"
            );

            Console.WriteLine($"---> GETTING trolley '{userId}' ....");

            return await _httpClient.SendAsync(_request);
        }




        private void InitializeHttpRequestMessage(HttpMethod method, string uri, HttpContent content = default)
        {
            _request = new HttpRequestMessage { RequestUri = new Uri(_baseUri + uri) };
            _request.Method = method;
            _request.Content = content;
            _request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
        }
    }
}
