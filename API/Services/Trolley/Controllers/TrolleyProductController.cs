using Business.Trolley.DTOs;
using Microsoft.AspNetCore.Mvc;
using Trolley.Services.Interfaces;



namespace Trolley.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyProductController : ControllerBase
    {

        private readonly ITrolleyProductService _trolleyProductsService;



        public TrolleyProductController(ITrolleyProductService trolleyProductsService)
        {
            _trolleyProductsService = trolleyProductsService;
        }






        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUsersTrolleyProducts([FromRoute] GetTrolleyProductsDTO getTrolleyProductsDTO)
        {
            var result = await _trolleyProductsService.GetTrolleyProducts(getTrolleyProductsDTO.UserId);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpPost("{UserId}")]
        public async Task<IActionResult> AddProductsToUsersTrolley([FromRoute] AddProductsToTrolleyDTO user, [FromBody] AddProductsToTrolleyDTO data)
        {
            // UserId in DTO is nullable, initialized by route value:
            var result = await _trolleyProductsService.AddProductsToTrolley(user.UserId ?? 0, data.Products);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpDelete("{UserId}")]
        public async Task<IActionResult> RemoveUsersTrolleyProducts([FromRoute] RemoveTrolleyProductsDTO user, [FromBody] RemoveTrolleyProductsDTO data)
        {
            var result = await _trolleyProductsService.RemoveProductsFromTrolley(user.UserId ?? 0, data.Products);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpDelete("{UserId}/delete")]
        public async Task<IActionResult> DeleteUsersTrolleyProducts([FromRoute] DeleteTrolleyProductsDTO user, [FromBody] DeleteTrolleyProductsDTO data)
        {
            var result = await _trolleyProductsService.DeleteProductsFromTrolley(user.UserId ?? 0, data.Products);

            return result.Status ? Ok(result) : BadRequest(result);
        }


    }
}
