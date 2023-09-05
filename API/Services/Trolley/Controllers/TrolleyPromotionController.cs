using Microsoft.AspNetCore.Mvc;
using Trolley.Services.Interfaces;



namespace Trolley.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyPromotionController : ControllerBase
    {

        private readonly ITrolleyPromotionService _trolleyPromotionService;


        public TrolleyPromotionController(ITrolleyPromotionService trolleyPromotionService)
        {
            _trolleyPromotionService = trolleyPromotionService;
        }






        [HttpGet]
        public async Task<IActionResult> GetAllTrolleyPromotions()
        {
            var result = await _trolleyPromotionService.GetAllTrolleyPromotions();

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet("active")]
        public async Task<IActionResult> GetActiveTrolleyPromotions()
        {
            var result = await _trolleyPromotionService.GetActiveTrolleyPromotions();

            return result.Status ? Ok(result) : BadRequest(result);
        }



    }
}
