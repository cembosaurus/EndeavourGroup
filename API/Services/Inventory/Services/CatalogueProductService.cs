using AutoMapper;
using Business.Inventory.DTOs.CatalogueProduct;
using Services.Inventory.Models;
using Business.Libraries.ServiceResult.Interfaces;
using Inventory.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Inventory.Data.Repositories.Interfaces;



namespace Inventory.Services
{
    public class CatalogueProductService: ICatalogueProductService
    {

        private readonly ICatalogueProductRepository _catalogueProductRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly IServiceResultFactory _resultFact;


        public CatalogueProductService(ICatalogueProductRepository catalogueProductRepo, IProductRepository productRepo,IMapper mapper, IServiceResultFactory resultFact)
        {
            _catalogueProductRepo = catalogueProductRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _resultFact = resultFact;
        }





        public async Task<IServiceResult<IEnumerable<CatalogueProductReadDTO>>> GetCatalogueProducts(IEnumerable<int> productIds = null)
        {
            Console.WriteLine($"--> GETTING catalogue products ......");

            var message = "";


            var catalogueProducts = await _catalogueProductRepo.GetCatalogueProducts(productIds);

            if (!catalogueProducts.Any())
                message = "NO catalogue products found !";

            return _resultFact.Result(_mapper.Map<IEnumerable<CatalogueProductReadDTO>>(catalogueProducts), true, message);
        }



        public async Task<IServiceResult<CatalogueProductReadDTO>> GetCatalogueProductById(int id)
        {
            Console.WriteLine($"--> GETTING catalogue product '{id}' ......");

            var message = "";


            var catalogueProduct = await _catalogueProductRepo.GetCatalogueProductById(id);

            if (catalogueProduct == null)
            {
                message = $"Catalogue Product with id '{id}' NOT found";

                if (await _productRepo.ExistsById(id))
                    message += Environment.NewLine + $", but Product with id '{id}' exists and it's not registered in catalogue !";
            }

            return _resultFact.Result(_mapper.Map<CatalogueProductReadDTO>(catalogueProduct), true, message);
        }



        public async Task<IServiceResult<bool>> ExistsCatalogueProductById(int id)
        {
            Console.WriteLine($"--> EXISTS catalogue product '{id}' ......");


            var catalogueProductResult = await _catalogueProductRepo.ExistsById(id);

            return _resultFact.Result(catalogueProductResult, true, catalogueProductResult ? "" : $"Catalogue product '{id}' does NOT exist !");
        }



        public async Task<IServiceResult<CatalogueProductReadDTO>> CreateCatalogueProduct(int productId, CatalogueProductCreateDTO catalogueProductCreateDTO)
        {
            if (await _catalogueProductRepo.ExistsById(productId))
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"Catalogue product with id '{productId}' already EXISTS !");

            var product = await _productRepo.GetProductById(productId);

            if (product == null)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"Product with id '{productId}' NOT found !");

            catalogueProductCreateDTO.Instock = (catalogueProductCreateDTO.Instock == null || catalogueProductCreateDTO.Instock < 0) ? 0 : catalogueProductCreateDTO.Instock;


            Console.WriteLine($"--> CREATING catalogue product '{productId}'......");


            var catalogueProduct = _mapper.Map<CatalogueProduct>(product);

            _mapper.Map(catalogueProductCreateDTO, catalogueProduct);

            var resultState = await _catalogueProductRepo.CreateCatalogueProduct(catalogueProduct);

            if(resultState != EntityState.Added || _catalogueProductRepo.SaveChanges() < 1)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"Catalogue product with product id '{productId}' was NOT created !");

            return _resultFact.Result(_mapper.Map<CatalogueProductReadDTO>(catalogueProduct), true);
        }




        public async Task<IServiceResult<CatalogueProductReadDTO>> UpdateCatalogueProduct(int productId, CatalogueProductUpdateDTO catalogueProductUpdateDTO)
        {
            var catalogueProduct = await _catalogueProductRepo.GetCatalogueProductById(productId);

            if (catalogueProduct == null)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"Catalogue product with id '{productId}' NOT found !");

            catalogueProductUpdateDTO.Instock = (catalogueProductUpdateDTO.Instock == null || catalogueProductUpdateDTO.Instock < 0) ? 0 : catalogueProductUpdateDTO.Instock;


            Console.WriteLine($"--> UPDATING catalogue product '{catalogueProduct.ProductId}': '{catalogueProduct.Product.Name}'......");


            _mapper.Map(catalogueProductUpdateDTO, catalogueProduct);

            if (_catalogueProductRepo.SaveChanges() < 1)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"Catalogue product '{productId}': changes were NOT saved into DB !");

            return _resultFact.Result(_mapper.Map<CatalogueProductReadDTO>(catalogueProduct), true);
        }
   



        public async Task<IServiceResult<CatalogueProductReadDTO>> RemoveCatalogueProduct(int id)
        {
            var catalogueProductToRemove = await _catalogueProductRepo.GetCatalogueProductById(id);

            if (catalogueProductToRemove == null)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"Catalogue product with id '{id}' was NOT found !");


            Console.WriteLine($"--> REMOVING catalogue product '{catalogueProductToRemove.ProductId}': '{catalogueProductToRemove.Product.Name}'......");


            var resultState = await _catalogueProductRepo.RemoveCatalogueProduct(catalogueProductToRemove);

            if (resultState != EntityState.Deleted || _catalogueProductRepo.SaveChanges() < 1)
                return _resultFact.Result<CatalogueProductReadDTO>(null, false, $"Catalogue product with product id '{catalogueProductToRemove.ProductId}' was NOT removed !");

            return _resultFact.Result(_mapper.Map<CatalogueProductReadDTO>(catalogueProductToRemove), true);
        }




        public async Task<IServiceResult<int>> GetInstockCount(int id)
        {
            Console.WriteLine($"--> GETTING catalogue product '{id}' instock count ......");

            var message = "";


            if (!await _catalogueProductRepo.ExistsById(id))
            {
                message = $"Catalogue Product with id '{id}' NOT found";

                if (await _productRepo.ExistsById(id))
                    message += $", but Product with id '{id}' exists and it's not registered in catalogue !";

                return _resultFact.Result(0, false, message);
            }

            var instock = await _catalogueProductRepo.GetInstockCount(id);

            return _resultFact.Result(instock, true);
        }



        public async Task<IServiceResult<int>> RemoveFromStockAmount(int id, int amount)
        {
            Console.WriteLine($"--> REMOVING product '{id}' amount '{amount}' from stock ......");


            var catalogueProduct = await _catalogueProductRepo.GetCatalogueProductById(id);

            if (catalogueProduct == null)
            {
                var message = $"Catalogue Product with id '{id}' NOT found";

                if (await _productRepo.ExistsById(id))
                    return _resultFact.Result(0, false, message + ", but Product with this id exists !");

                return _resultFact.Result(0, false, message + " !");
            }

            var instock = await _catalogueProductRepo.GetInstockCount(id);

            catalogueProduct.Instock = amount > instock ? 0 : catalogueProduct.Instock - amount;

            if (_catalogueProductRepo.SaveChanges() < 1)
                return _resultFact.Result(0, false, $"Catalogue product '{id}' instock amount was NOT changed !");

            return _resultFact.Result(catalogueProduct.Instock, true, instock - amount < 0 ? $"Insufficient amount in stock ! Only {instock} catalogue products were removed from stock !" : null);
        }




        public async Task<IServiceResult<int>> AddAmountToStock(int productId, int amount)
        {
            if (amount < 0)
                return _resultFact.Result(0, false, "Only positive number can be added to stock amount !");


            Console.WriteLine($"--> ADDING product '{productId}' amount '{amount}' to stock ......");


            var catalogueProduct = await _catalogueProductRepo.GetCatalogueProductById(productId);

            if (catalogueProduct == null)
            {
                var message = $"Catalogue Product with id '{productId}' NOT found";

                if (await _productRepo.ExistsById(productId))
                    return _resultFact.Result(0, false, message + ", but Product with this id exists !");

                return _resultFact.Result(0, false, message + " !");
            }

            catalogueProduct.Instock += amount;

            if (_catalogueProductRepo.SaveChanges() < 1)
                return _resultFact.Result(0, false, $"Catalogue product '{productId}' instock amount was NOT changed !");

            return _resultFact.Result(catalogueProduct.Instock, true);
        }
    }
}
