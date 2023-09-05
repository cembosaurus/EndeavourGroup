using Business.Trolley.DTOs;
using Business.Trolley.Http.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;



namespace Business.Trolley.Http
{
    public class HttpTrolleyProductClient : IHttpTrolleyProductClient
    {

        private HttpRequestMessage _request;
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _mediaType = "application/json";


        public HttpTrolleyProductClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = config.GetSection("RemoteServices:TrolleyService").Value + "/api/trolley";
        }





        public async Task<HttpResponseMessage> AddProductsToTrolley(int userId, IEnumerable<TrolleyProductUpdateDTO> products)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Post,
                $"/{userId}/products",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new { products }), _encoding, _mediaType)
            );


            Console.WriteLine($"---> ADDING products to trolley '{userId}' ....");

            return await _httpClient.SendAsync(_request);
        }




        public async Task<HttpResponseMessage> DeleteTrolleyProducts(int userId, IEnumerable<int> products)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Delete,
                $"/{userId}/products/delete",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new { products }), _encoding, _mediaType)
            );

            Console.WriteLine($"---> DELETING products from trolley '{userId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetAllTrolleyProducts()
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/products/all"
            );

            Console.WriteLine($"---> GETTING all trolley products ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetTrolleyProducts(int userId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/{userId}/products"
            );

            Console.WriteLine($"---> GETTING trolley products for trolley '{userId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> RemoveTrolleyProducts(int trolleyId, IEnumerable<TrolleyProductUpdateDTO> products)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Delete,
                $"/{trolleyId}/products",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new { products }), _encoding, _mediaType)
            );

            Console.WriteLine($"---> REMOVING trolley products from trolley '{trolleyId}' ....");

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
