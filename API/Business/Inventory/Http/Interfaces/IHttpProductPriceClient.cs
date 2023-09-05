using Business.Inventory.DTOs.ProductPrice;



namespace Business.Inventory.Http.Interfaces
{
    public interface IHttpProductPriceClient
    {
        Task<HttpResponseMessage> GetProductPriceById(int productId);
        Task<HttpResponseMessage> GetProductPrices(IEnumerable<int> productIds);
        Task<HttpResponseMessage> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceUpdateDTO);
    }
}
