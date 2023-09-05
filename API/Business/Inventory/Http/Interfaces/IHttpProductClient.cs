using Business.Inventory.DTOs.Product;



namespace Business.Inventory.Http.Interfaces
{
    public interface IHttpProductClient
    {
        Task<HttpResponseMessage> AddProduct(ProductCreateDTO productDTO);
        Task<HttpResponseMessage> DeleteProduct(int productId);
        Task<HttpResponseMessage> GetProductById(int productId);
        Task<HttpResponseMessage> GetProducts(IEnumerable<int> productIds = null);
        Task<HttpResponseMessage> UpdateProduct(int productId, ProductUpdateDTO productDTO);
    }
}
