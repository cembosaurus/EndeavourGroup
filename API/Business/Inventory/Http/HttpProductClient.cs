using Business.Inventory.DTOs.Product;
using Business.Inventory.Http.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;



namespace Business.Inventory.Http
{
    public class HttpProductClient : IHttpProductClient
    {

        private HttpRequestMessage _request;
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _mediaType = "application/json";


        public HttpProductClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = config.GetSection("RemoteServices:InventoryService").Value + "/api/catalogueproduct";
        }




        public async Task<HttpResponseMessage> GetProducts(IEnumerable<int> productIds = default)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"{(productIds != null && productIds.Any() ? "" : "/all")}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(productIds), _encoding, _mediaType)
            );

            Console.WriteLine($"---> GETTING Products .....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetProductById(int productId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/{productId}"
            );

            Console.WriteLine($"---> GETTING Product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> AddProduct(ProductCreateDTO productDTO)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Post,
                $"",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(productDTO), _encoding, _mediaType)
            );

            Console.WriteLine($"---> ADDING Product '{productDTO.Name}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> DeleteProduct(int productId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Delete,
                $"{_baseUri}/{productId}"
            );

            Console.WriteLine($"---> DELETING Product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> UpdateProduct(int productId, ProductUpdateDTO productDTO)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Put,
                $"{_baseUri}/{productId}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(productDTO), _encoding, _mediaType)
            );

            Console.WriteLine($"---> UPDATING Product '{productId}': '{productDTO.Name}' ....");

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
