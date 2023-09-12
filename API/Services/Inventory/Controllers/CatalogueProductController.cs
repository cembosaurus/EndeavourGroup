using Business.Inventory.DTOs.CatalogueProduct;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace Services.Inventory.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueProductController : ControllerBase
    {

        private readonly ICatalogueProductService _catalogueProductService;

        public CatalogueProductController(ICatalogueProductService catalogueProductService)
        {
            _catalogueProductService = catalogueProductService;
        }






        // GET:
        [HttpGet("all")]
        public async Task<ActionResult> GetAllCatalogueProducts()
        {
            var result = await _catalogueProductService.GetCatalogueProducts();

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet]
        public async Task<ActionResult> GetCatalogueProducts([FromBody]IEnumerable<int> productIds)
        {
            var result = await _catalogueProductService.GetCatalogueProducts(productIds);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet("{productId}")]
        public async Task<ActionResult> GetCatalogueProductById(int productId)
        {
            var result = await _catalogueProductService.GetCatalogueProductById(productId);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet("{productId}/exists")]
        public async Task<ActionResult> ExistsCatalogueProductById(int productId)
        {
            var result = await _catalogueProductService.ExistsCatalogueProductById(productId);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet("{productId}/instock")]
        public async Task<ActionResult> GetInstockCount(int productId)
        {
            var result = await _catalogueProductService.GetInstockCount(productId);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        // POST:

        [HttpPost("{productId}")]
        public async Task<ActionResult> CreateCatalogueProduct(int productId, CatalogueProductCreateDTO catalogueProductCreateDTO)
        {
            //var result = await _catalogueProductService.CreateCatalogueProduct(productId, catalogueProductCreateDTO);

            //return result.Status ? Ok(result) : BadRequest(result);

            return Ok(catalogueProductCreateDTO);
        }




        // PUT:

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateCatalogueProduct(int productId, CatalogueProductUpdateDTO catalogueProductUpdateDTO)
        {
            var result = await _catalogueProductService.UpdateCatalogueProduct(productId, catalogueProductUpdateDTO);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpPut("{productId}/fromstock/{amount}")]
        public async Task<ActionResult> RemoveFromStockAmount(int productId, int amount)
        {
            var result = await _catalogueProductService.RemoveFromStockAmount(productId, amount);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpPut("{productId}/tostock/{amount}")]
        public async Task<ActionResult> AddAmountToStock(int productId, int amount)
        {
            var result = await _catalogueProductService.AddAmountToStock(productId, amount);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        // DELETE:

        [HttpDelete("{productId}")]
        public async Task<ActionResult> RemoveCatalogueProduct(int productId)
        {
            var result = await _catalogueProductService.RemoveCatalogueProduct(productId);

            return result.Status ? Ok(result) : BadRequest(result);
        }



    }
}
