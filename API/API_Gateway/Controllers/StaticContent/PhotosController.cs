using Microsoft.AspNetCore.Mvc;



namespace API_Gateway.Controllers.StaticContent
{

    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private readonly string _staticContentBaseUrl;
        private readonly string _staticContentProductsUrl;


        public PhotosController(IConfiguration config)
        {
            _staticContentBaseUrl = config.GetSection("RemoteServices:StaticContentService:BaseURL").Value;
            _staticContentProductsUrl = config.GetSection("RemoteServices:StaticContentService:ProductsURL").Value;
        }





        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Redirect($"{_staticContentBaseUrl}/{_staticContentProductsUrl}/{id}");
        }



    }
}
