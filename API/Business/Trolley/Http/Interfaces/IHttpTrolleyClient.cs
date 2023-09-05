

namespace Business.Trolley.Http.Interfaces
{
    public interface IHttpTrolleyClient
    {
        Task<HttpResponseMessage> CreateTrolley(int userId);
        Task<HttpResponseMessage> DeleteTrolley(int id);
        Task<HttpResponseMessage> ExistsTrolleyByTrolleyId(Guid trolleyId);
        Task<HttpResponseMessage> GetTrolleys();
        Task<HttpResponseMessage> GetTrolleyByUserId(int userId);
    }
}
