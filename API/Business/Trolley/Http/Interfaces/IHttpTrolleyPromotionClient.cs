

namespace Business.Trolley.Http.Interfaces
{
    public interface IHttpTrolleyPromotionClient
    {
        Task<HttpResponseMessage> GetAllTrolleyPromotions();
        Task<HttpResponseMessage> GetActiveTrolleyPromotions();
    }
}
