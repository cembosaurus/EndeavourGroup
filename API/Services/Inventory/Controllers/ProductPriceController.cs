using Business.Inventory.DTOs.ProductPrice;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace Services.Inventory.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceController : ControllerBase
    {

        private readonly IProductPriceService _productPriceService;


        public ProductPriceController(IProductPriceService productPriceservice)
        {
            _productPriceService = productPriceservice;
        }






        [HttpGet("all")]
        public async Task<ActionResult> GetAllProductPrices()
        {
            var result = await _productPriceService.GetProductPrices();

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet]
        public async Task<ActionResult> GetProductPrices(IEnumerable<int> productIds)
        {
            var result = await _productPriceService.GetProductPrices(productIds);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet("{productId}", Name = "GetProductPriceById")]
        public async Task<ActionResult> GetProductPriceById(int productId)
        {
            var result = await _productPriceService.GetProductPriceById(productId);

            return result.Status ? Ok(result) : BadRequest(result);
        }




        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceEditDTO)
        {
            var result = await _productPriceService.UpdateProductPrice(productId, productPriceEditDTO);

            return result.Status ? Ok(result) : BadRequest(result);
        }

    }
}
