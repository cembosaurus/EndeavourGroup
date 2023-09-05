using Business.Trolley.Http.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;



namespace Business.Trolley.Http
{
    public class HttpTrolleyPromotionClient : IHttpTrolleyPromotionClient
    {

        private HttpRequestMessage _request;
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly string _mediaType = "application/json";


        public HttpTrolleyPromotionClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = config.GetSection("RemoteServices:TrolleyService").Value + "/api/trolleypromotion";
        }







        public async Task<HttpResponseMessage> GetActiveTrolleyPromotions()
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/active"
            );

            Console.WriteLine($"---> GETTING active promotions ....");

            return await _httpClient.SendAsync(_request);
        }
          



        public async Task<HttpResponseMessage> GetAllTrolleyPromotions()
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $""
            );

            Console.WriteLine($"---> GETTING all promotions ....");

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
