using Business.Trolley.DTOs;
using Microsoft.AspNetCore.Mvc;
using Trolley.Services.Interfaces;



namespace Trolley.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {

        private readonly ITrolleyService _trolleyService;



        public TrolleyController(ITrolleyService trolleyService)
        {
            _trolleyService = trolleyService;
        }






        [HttpGet]
        public async Task<IActionResult> GetTrolleys()
        {
            var result = await _trolleyService.GetTrolleys();

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUsersTrolley([FromRoute] GetTrolleyDTO getTrolleyDTO)
        {
            var result = await _trolleyService.GetTrolleyByUserId(getTrolleyDTO.UserId ?? 0);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpGet("exists")]
        public async Task<IActionResult> ExistsTrolleyByTrolleyId([FromBody] ExistsTrolleyByTrolleyIdDTO existsTrolleyByTrolleyIdDTO)
        {
            var result = await _trolleyService.ExistsTrolleyByTrolleyId(existsTrolleyByTrolleyIdDTO.TrolleyId);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpPost("{UserId}")]
        public async Task<IActionResult> CreateTrolley([FromRoute] CreateTrolleyDTO createTrolleyDTO)
        {
            var result = await _trolleyService.CreateTrolley(createTrolleyDTO.UserId);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUsersTrolley([FromRoute] DeleteTrolleyDTO user)
        {
            var result = await _trolleyService.DeleteTrolley(user.UserId);

            return result.Status ? Ok(result) : BadRequest(result);
        }


    }
}
