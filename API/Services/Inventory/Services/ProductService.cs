using AutoMapper;
using Business.Inventory.DTOs.Product;
using Business.Libraries.ServiceResult.Interfaces;
using Inventory.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Inventory.Data.Repositories.Interfaces;
using Services.Inventory.Models;



namespace Inventory.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly IServiceResultFactory _resultFact;


        public ProductService(IProductRepository repo, IMapper mapper, IServiceResultFactory resultFact)
        {
            _repo = repo;
            _mapper = mapper;
            _resultFact = resultFact;
        }





        public async Task<IServiceResult<IEnumerable<ProductReadDTO>>> GetProducts(IEnumerable<int> productIds = null)
        {
            Console.WriteLine($"--> GETTING products ......");

            var message = "";


            var products = await _repo.GetProducts(productIds);

            if (!products.Any())
                message = "NO products found !";

            return _resultFact.Result(_mapper.Map<IEnumerable<ProductReadDTO>>(products), true, message);
        }



        public async Task<IServiceResult<ProductReadDTO>> GetProductById(int id)
        {
            Console.WriteLine($"--> GETTING product '{id}' ......");

            var message = "";


            var product = await _repo.GetProductById(id);

            if (product == null)
                message = $"Product '{id}' was NOT found !";

            return _resultFact.Result(_mapper.Map<ProductReadDTO>(product), true, message);
        }



        public async Task<IServiceResult<ProductReadDTO>> AddProduct(ProductCreateDTO productCreateDTO)
        {
            Console.WriteLine($"--> ADDING product '{productCreateDTO.Name}'......");


            if (await _repo.ExistsByName(productCreateDTO.Name))
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product '{productCreateDTO.Name}' already EXISTS !");

            var product = _mapper.Map<Product>(productCreateDTO);

            var resultState = await _repo.AddProduct(product);


            if (resultState != EntityState.Added || _repo.SaveChanges() < 1)
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product '{productCreateDTO.Name}' was NOT created");

            return _resultFact.Result(_mapper.Map<ProductReadDTO>(product), true);
        }



        public async Task<IServiceResult<ProductReadDTO>> UpdateProduct(int id, ProductUpdateDTO productUpdateDTO)
        {
            var product = await _repo.GetProductById(id);

            if (product == null)
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product '{id}' NOT found !");
            if(await _repo.ExistsByName(productUpdateDTO.Name))
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product with name: '{productUpdateDTO.Name}' already exists !");


            Console.WriteLine($"--> UPDATING product '{product.Id}': '{product.Name}'......");


            _mapper.Map(productUpdateDTO, product);

            if (_repo.SaveChanges() < 1)
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product '{id}': changes were NOT saved into DB !");

            return _resultFact.Result(_mapper.Map<ProductReadDTO>(product), true);
        }



        public async Task<IServiceResult<ProductReadDTO>> DeleteProduct(int id)
        {
            var product = await _repo.GetProductById(id);

            if (product == null)
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product '{id}' NOT found !");

            if(product.Archived)
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product '{id}' can NOT be deleted because it is ARCHIVED !");


            Console.WriteLine($"--> DELETING product '{id}'......");


            var resultState = await _repo.DeleteProduct(product);

            if (resultState != EntityState.Deleted || _repo.SaveChanges() < 1)
                return _resultFact.Result<ProductReadDTO>(null, false, $"Product with id '{id}' was NOT removed from DB !");

            return _resultFact.Result(_mapper.Map<ProductReadDTO>(product), true);
        }

    }
}
