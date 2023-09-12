using Business.Inventory.DTOs.CatalogueProduct;
using Business.Inventory.Http.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;



namespace Business.Inventory.Http
{
    public class HttpCatalogueProductClient : IHttpCatalogueProductClient
    {

        private HttpRequestMessage _request;
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _mediaType = "application/json";



        public HttpCatalogueProductClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = config.GetSection("RemoteServices:InventoryService").Value + "/api/catalogueproduct";
        }



        // multiple method call error FIXED: HttpRequestMessage - CAN NOT be reused. It doesn't change streamed content on .SendAsync() !!!!




        public async Task<HttpResponseMessage> GetCatalogueProducts(IEnumerable<int> productIds = default)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"{(productIds != null && productIds.Any() ? "" : "/all")}",
                new StringContent(JsonConvert.SerializeObject(productIds), _encoding, _mediaType)
            );

            Console.WriteLine($"---> GETTING Catalogue products ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetCatalogueProductById(int productId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/{productId}"
            );

            Console.WriteLine($"---> GETTING Catalogue product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> ExistsCatalogueProductById(int productId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/{productId}/exists"
            );

            Console.WriteLine($"---> EXISTS Catalogue product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> CreateCatalogueProduct(int productId, CatalogueProductCreateDTO catalogueProductCreateDTO)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Post,
                $"/{productId}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(catalogueProductCreateDTO), _encoding, _mediaType)
            );

            Console.WriteLine($"---> CREATING Catalogue product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> UpdateCatalogueProduct(int productId, CatalogueProductUpdateDTO catalogueProductUpdateDTO)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Post,
                $"/{productId}",
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(catalogueProductUpdateDTO), _encoding, _mediaType)
            );

            Console.WriteLine($"---> UPDATING Catalogue product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> RemoveCatalogueProduct(int productId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Delete,
                $"/{productId}"
            );

            Console.WriteLine($"---> REMOVING Catalogue product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> GetInstockCount(int productId)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Get,
                $"/{productId}/instock"
            );

            Console.WriteLine($"---> GETTING Instock amount for catalogue product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> RemoveFromStockAmount(int productId, int amount)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Put,
                $"/{productId}/fromstock/{amount}"
            );

            Console.WriteLine($"---> REMOVING amount '{amount}' from stock for catalogue product '{productId}' ....");

            return await _httpClient.SendAsync(_request);
        }



        public async Task<HttpResponseMessage> AddAmountToStock(int productId, int amount)
        {
            InitializeHttpRequestMessage(
                HttpMethod.Put,
                $"/{productId}/tostock/{amount}"
            );

            Console.WriteLine($"---> ADDING amount '{amount}' to stock for catalogue product '{productId}' ....");

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
