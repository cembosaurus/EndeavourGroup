using StaticContent.Services.Interfaces;



namespace StaticContent.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IImageFilesService _imageFilesService;


        public PhotosController(IImageFilesService imageFilesService)
        {
            _imageFilesService = imageFilesService;
        }




        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var image = _imageFilesService.GetById(id);

            return File(image, "image/jpeg");
        }



    }
}
