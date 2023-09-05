using AutoMapper;
using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult.Interfaces;
using Inventory.Services.Interfaces;
using Services.Inventory.Data.Repositories.Interfaces;



namespace Inventory.Services
{
    public class ProductPriceService: IProductPriceService
    {

        private readonly IProductPriceRepository _repo;
        private readonly IMapper _mapper;
        private readonly IServiceResultFactory _resultFact;


        public ProductPriceService(IProductPriceRepository repo, IMapper mapper, IServiceResultFactory resultFact)
        {
            _repo = repo;
            _mapper = mapper;
            _resultFact = resultFact;
        }




        public async Task<IServiceResult<IEnumerable<ProductPriceReadDTO>>> GetProductPrices(IEnumerable<int> productIds = default)
        {
            Console.WriteLine($"--> GETTING product prices ......");


            var productPrices = await _repo.GetProductPrices(productIds);

            if (productPrices == null || !productPrices.Any())
                return _resultFact.Result<IEnumerable<ProductPriceReadDTO>>(null, true, "NO product prices found !");


            return _resultFact.Result(
                _mapper.Map<IEnumerable<ProductPriceReadDTO>>(productPrices), 
                true, 
                $"{(productIds == null ? "" : (productIds.Count() > productPrices.Count() ? $"Prices for {productIds.Count() - productPrices.Count()} products were not found ! Reason: Products may not be registered in catalogue." : ""))}");
        }



        public async Task<IServiceResult<ProductPriceReadDTO>> GetProductPriceById(int id)
        {
            Console.WriteLine($"--> GETTING product price '{id}' ......");

            var message = "";


            var productPrice = await _repo.GetProductPriceById(id);

            if (productPrice == null)
            {
                message = $"Catalogue product with Id '{id}' NOT found";

                if (await _repo.ProductExistsById(id))
                    message += $", but Product with Id '{id}' EXIST and is NOT labeled with price";

                return _resultFact.Result<ProductPriceReadDTO>(null, false, message + " !");
            }

            return _resultFact.Result(_mapper.Map<ProductPriceReadDTO>(productPrice), true, message);
        }




        public async Task<IServiceResult<ProductPriceReadDTO>> UpdateProductPrice(int productId, ProductPriceUpdateDTO productPriceEditDTO)
        {
            Console.WriteLine($"--> UPDATING product price '{productId}' ......");


            var productPrice = await _repo.GetProductPriceById(productId);

            if (productPrice == null)
                return _resultFact.Result<ProductPriceReadDTO>(null, false, $"Product price '{productId}': NOT found !");

            _mapper.Map(productPriceEditDTO, productPrice);

            if (_repo.SaveChanges() < 1)
                return _resultFact.Result<ProductPriceReadDTO>(null, false, $"Product price '{productId}': changes were NOT saved into DB !");

            return _resultFact.Result(_mapper.Map<ProductPriceReadDTO>(productPrice), true);
        }


    }
}
