using API_Gateway.HttpServices.Trolley;
using API_Gateway.HttpServices.Trolley.Interfaces;
using API_Gateway.Services.Trolley.Interfaces;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;



namespace API_Gateway.Services.Trolley
{
    public class TrolleyService : ITrolleyService
    {

        private readonly IHttpTrolleyService _httpTrolleyService;
        private readonly IHttpTrolleyPromotionService _httpTrolleyPromotionService;

        public TrolleyService(IHttpTrolleyService httpTrolleyService, IHttpTrolleyPromotionService httpTrolleyPromotionService)
        {
            _httpTrolleyService = httpTrolleyService;
            _httpTrolleyPromotionService = httpTrolleyPromotionService;
        }





        public async Task<IServiceResult<IEnumerable<TrolleyReadDTO>>> GetTrolleys()
        {
            return await _httpTrolleyService.GetTrolleys();
        }



        public async Task<IServiceResult<TrolleyReadDTO>> GetTrolleyByUserId(int userId)
        {
            return await _httpTrolleyService.GetTrolleyByUserId(userId);
        }



        public async Task<IServiceResult<TrolleyReadDTO>> GetUsersTrolleyDiscounted(int userId)
        {
            var trolley = await _httpTrolleyService.GetTrolleyByUserId(userId);
            var discounts = await _httpTrolleyPromotionService.GetActiveTrolleyPromotions();

            //............................................................................................... To Do: apply active trolley promotions to trolley ......

            return null;
        }



        public async Task<IServiceResult<TrolleyReadDTO>> CreateTrolley(int userId)
        {
            return await _httpTrolleyService.CreateTrolley(userId);
        }



        public async Task<IServiceResult<TrolleyReadDTO>> DeleteTrolley(int userId)
        {
            return await _httpTrolleyService.DeleteTrolley(userId);
        }




        public async Task<IServiceResult<bool>> ExistsTrolleyByTrolleyId(Guid trolleyId)
        {
            return await _httpTrolleyService.ExistsTrolleyByTrolleyId(trolleyId);
        }


    }
}
