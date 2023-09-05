using Business.Inventory.Controllers.Interfaces;
using Business.Inventory.DTOs.Product;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace Services.Inventory.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase, IProductController
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }





        [HttpGet("all")]
        public async Task<ActionResult> GetAllProducts()
        {
            var result = await _productService.GetProducts();

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet]
        public async Task<ActionResult> GetProducts(IEnumerable<int> productIds)
        {
            var result = await _productService.GetProducts(productIds);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductById(id);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductCreateDTO productCreateDTO)
        {
           var result = await _productService.AddProduct(productCreateDTO);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateDTO productUpdateDTO)
        {
            var result = await _productService.UpdateProduct(id, productUpdateDTO);

            return result.Status ? Ok(result) : BadRequest(result);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductById(int id)
        {
            var result = await _productService.DeleteProduct(id);

            return result.Status ? Ok(result) : BadRequest(result);
        }


    }
}
