using API_Gateway.HttpServices.Trolley.Interfaces;
using API_Gateway.Services.Trolley.Interfaces;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;

namespace API_Gateway.Services.Trolley
{
    public class TrolleyPromotionService : ITrolleyPromotionService
    {

        private readonly IHttpTrolleyPromotionService _httpTrolleyPromotionService;
        private readonly IServiceResultFactory _resultFact;


        public TrolleyPromotionService(IHttpTrolleyPromotionService httpTrolleyPromotionService, IServiceResultFactory resultFact)
        {
            _httpTrolleyPromotionService = httpTrolleyPromotionService;
            _resultFact = resultFact;
        }




        public async Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetAllTrolleyPromotions()
        {
            return await _httpTrolleyPromotionService.GetAllTrolleyPromotions();
        }



        public async Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetActiveTrolleyPromotions()
        {
            return await _httpTrolleyPromotionService.GetActiveTrolleyPromotions();
        }

    }
}
