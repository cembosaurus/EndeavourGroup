using API_Gateway.Services.Trolley.Interfaces;
using Business.Trolley.DTOs;
using Microsoft.AspNetCore.Mvc;



namespace API_Gateway.Controllers.Trolley
{

    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {

        private readonly ITrolleyService _trolleyService;
        private readonly ITrolleyProductService _trolleyProductService;
        private readonly ITrolleyPromotionService _trolleyPromotionService;

        public TrolleyController(ITrolleyService trolleyService, ITrolleyProductService trolleyProductService, ITrolleyPromotionService trolleyPromotionService)
        {
            _trolleyService = trolleyService;
            _trolleyProductService = trolleyProductService;
            _trolleyPromotionService = trolleyPromotionService;
        }



        // .... To Do: separate domain into 3 controllers Trolley, TrolleyProducts and TrolleyPromotions so the URL mathces the remote services' URL. Easier to debug.



        // GET:

        // Trolley:

        [HttpGet("all")]
        public async Task<IActionResult> GetTrolleys()
        {
            var result = await _trolleyService.GetTrolleys();

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUsersTrolley([FromRoute] GetTrolleyDTO getTrolleyDTO)
        {
            var result = await _trolleyService.GetTrolleyByUserId(getTrolleyDTO.UserId ?? 0);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        // Trolley Products:

        [HttpGet("{userId}/products")]
        public async Task<IActionResult> GetUsersTrolleyProducts([FromRoute] GetTrolleyProductsDTO getTrolleyProductsDTO)
        {
            var result = await _trolleyProductService.GetTrolleyProducts(getTrolleyProductsDTO.UserId);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        // Trolley Promotions:

        [HttpGet("promotions")]
        public async Task<IActionResult> GetAllTrolleyPromotions()
        {
            var result = await _trolleyPromotionService.GetAllTrolleyPromotions();

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet("promotions/active")]
        public async Task<IActionResult> GetActiveTrolleyPromotions()
        {
            var result = await _trolleyPromotionService.GetActiveTrolleyPromotions();

            return result.Status ? Ok(result) : BadRequest(result);
        }



        // Trolley with Promotions:


        [HttpGet("{UserId}/discounted")]
        public async Task<IActionResult> GetUsersTrolleyDiscounted([FromRoute] GetTrolleyDTO getTrolleyDTO)
        {
            var result = await _trolleyService.GetUsersTrolleyDiscounted(getTrolleyDTO.UserId ?? 0);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        // POST:

        // Trolley:


        [HttpPost("{UserId}")]
        public async Task<IActionResult> CreateTrolley([FromRoute] CreateTrolleyDTO createTrolleyDTO)
        {
            var result = await _trolleyService.CreateTrolley(createTrolleyDTO.UserId);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        // Trolley Products:

        [HttpPost("{UserId}/products")]
        public async Task<IActionResult> AddProductsToUsersTrolley([FromRoute] AddProductsToTrolleyDTO user, [FromBody] AddProductsToTrolleyDTO data)
        {
            // UserId in DTO is nullable, initialized by route value:
            var result = await _trolleyProductService.AddProductsToTrolley(user.UserId ?? 0, data.Products);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        // DELETE:

        // Trolley:


        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUsersTrolley([FromRoute] DeleteTrolleyDTO user)
        {
            var result = await _trolleyService.DeleteTrolley(user.UserId);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        // Trolley Products:


        [HttpDelete("{UserId}/products")]
        public async Task<IActionResult> RemoveUsersTrolleyProducts([FromRoute] RemoveTrolleyProductsDTO user, [FromBody] RemoveTrolleyProductsDTO data)
        {
            var result = await _trolleyProductService.RemoveTrolleyProducts(user.UserId ?? 0, data.Products);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpDelete("{UserId}/products/delete")]
        public async Task<IActionResult> DeleteUsersTrolleyProducts([FromRoute] DeleteTrolleyProductsDTO user, [FromBody] DeleteTrolleyProductsDTO data)
        {
            var result = await _trolleyProductService.DeleteTrolleyProducts(user.UserId ?? 0, data.Products);

            return result.Status ? Ok(result) : BadRequest(result);
        }



    }
}
