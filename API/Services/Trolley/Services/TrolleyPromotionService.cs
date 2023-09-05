using AutoMapper;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Trolley.Data.Repositories.Interfaces;
using Trolley.Services.Interfaces;



namespace Trolley.Services
{
    public class TrolleyPromotionService : ITrolleyPromotionService
    {

        private readonly ITrolleyPromotionsRepository _trolleyPromotionsRepo;
        private readonly IServiceResultFactory _resultFact;
        private readonly IMapper _mapper;



        public TrolleyPromotionService(ITrolleyPromotionsRepository trolleyPromotionsRepo, IServiceResultFactory resultFact, IMapper mapper)
        {
            _trolleyPromotionsRepo = trolleyPromotionsRepo;
            _resultFact = resultFact;
            _mapper = mapper;
        }






        public async Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetActiveTrolleyPromotions()
        {
            Console.WriteLine($"--> GETTING active trolley promotions ......");

            var message = "";


            var trolleyPromotions = await _trolleyPromotionsRepo.GetActiveTrolleyPromotions();

            if (trolleyPromotions == null)
                return _resultFact.Result<IEnumerable<TrolleyPromotionReadDTO>>(null, false, $"NO Active Trolley Promotions were found !");


            var result = _mapper.Map<ICollection<TrolleyPromotionReadDTO>>(trolleyPromotions);


            return _resultFact.Result(_mapper.Map<IEnumerable<TrolleyPromotionReadDTO>>(result), true, message);
        }




        public async Task<IServiceResult<IEnumerable<TrolleyPromotionReadDTO>>> GetAllTrolleyPromotions()
        {
            Console.WriteLine($"--> GETTING trolley promotions ......");

            var message = "";


            var trolleyPromotions = await _trolleyPromotionsRepo.GetAllTrolleyPromotions();

            if (trolleyPromotions == null)
                return _resultFact.Result<IEnumerable<TrolleyPromotionReadDTO>>(null, false, $"NO Trolley Promotions were found !");


            var result = _mapper.Map<ICollection<TrolleyPromotionReadDTO>>(trolleyPromotions);


            return _resultFact.Result(_mapper.Map<IEnumerable<TrolleyPromotionReadDTO>>(result), true, message);
        }
    }
}
