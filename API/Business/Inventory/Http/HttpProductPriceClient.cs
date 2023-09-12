using Business.Inventory.DTOs.ProductPrice;
using Business.Inventory.Http.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;



namespace Business.Inventory.Http
{
    public class HttpProductPriceClient : IHttpProductPriceClient
    {

        private HttpRequestMessage _request;
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _mediaType = "application/json";


        public HttpProductPriceClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = config.GetSection("RemoteServices:InventoryService").Value + "/api/productprice";
        }





        public async Task<HttpResponseMessage> GetProductPrices(IEnumerable<int> productIds)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"{(productIds != null && productIds.Any() ? "" : "/all")}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(productIds), _encoding, _mediaType)
            );

            Console.WriteLine("---> GETTING Product prices ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetProductPriceById(int productId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/{productId}"
            );

            Console.WriteLine($"---> GETTING Product price '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceUpdateDTO)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Put,
                $"/{productId}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(productPriceUpdateDTO), _encoding, _mediaType)
            );

            Console.WriteLine($"---> UPDATING Product price '{productId}' ....");

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
