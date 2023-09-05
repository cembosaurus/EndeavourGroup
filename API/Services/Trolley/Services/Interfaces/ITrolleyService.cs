using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;



namespace Trolley.Services.Interfaces
{
    public interface ITrolleyService
    {
        Task<IServiceResult<TrolleyReadDTO>> CreateTrolley(int userId);
        Task<IServiceResult<TrolleyReadDTO>> DeleteTrolley(int id);
        Task<IServiceResult<bool>> ExistsTrolleyByTrolleyId(Guid trolleyId);
        Task<IServiceResult<IEnumerable<TrolleyReadDTO>>> GetTrolleys();
        Task<IServiceResult<TrolleyReadDTO>> GetTrolleyByUserId(int userId);
    }
}
