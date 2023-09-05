using AutoMapper;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Trolley.Models;
using Trolley.Data.Repositories.Interfaces;
using Trolley.HttpServices.Interfaces;
using Trolley.Services.Interfaces;
using Trolley.TrolleyBusinessLogic.Interfaces;



namespace Trolley.Services
{
    public class TrolleyService : ITrolleyService
    {

        private readonly ITrolleyRepository _trolleyRepo;
        private readonly IHttpInventoryService _httpInventoryService;
        private readonly IServiceResultFactory _resultFact;
        private readonly IMapper _mapper;
        private readonly ITrolleyBusinessLogic _trolleyBusinessLogic;


        public TrolleyService(ITrolleyRepository trolleyRepo, IServiceResultFactory resultFact, IMapper mapper, ITrolleyBusinessLogic trolleyBusinessLogic, IHttpInventoryService httpInventoryService)
        {
            _trolleyRepo = trolleyRepo;
            _httpInventoryService = httpInventoryService;
            _resultFact = resultFact;
            _mapper = mapper;
            _trolleyBusinessLogic = trolleyBusinessLogic;
        }





        public async Task<IServiceResult<IEnumerable<TrolleyReadDTO>>> GetTrolleys()
        {
            Console.WriteLine($"--> GETTING trolleys ......");

            var message = "";


            var trolleys = await _trolleyRepo.GetTrolleys();

            if (!trolleys.Any())
                message = "NO trolleys were found !";


            var result = _mapper.Map<IEnumerable<TrolleyReadDTO>>(trolleys);

            foreach (var t in result)
            {
                if(t.TrolleyProducts.Any())
                    await _trolleyBusinessLogic.GetCatalogueProductsIntoTrolley(t.TrolleyProducts);
            }


            return _resultFact.Result(result, true, message);
        }




        public async Task<IServiceResult<TrolleyReadDTO>> GetTrolleyByUserId(int userId)
        {
            var message = "";

            Console.WriteLine($"--> GETTING trolley for user '{userId}' ......");


            var trolley = await _trolleyRepo.GetTrolleyByUserId(userId);

            if (trolley == null)
                return _resultFact.Result<TrolleyReadDTO>(null, true, $"Trolley for user '{userId}' was NOT found !");

            var result = _mapper.Map<TrolleyReadDTO>(trolley);


            var productIds = trolley.TrolleyProducts.Select(i => i.ProductId).ToList();

            var productsResult = await _httpInventoryService.GetCatalogueProducts(productIds);

            if (productsResult != null || productsResult.Status)
            {
                foreach (var ci in result.TrolleyProducts)
                {
                    ci.Name = productsResult.Data.FirstOrDefault(i => i.Product.Id == ci.ProductId).Product.Name;

                    var productPriceResult = await _httpInventoryService.GetProductPriceById(ci.ProductId);

                    if (productPriceResult.Status)
                    {
                        ci.SalePrice = productPriceResult.Data.SalePrice;
                    }
                    else
                    {
                        message += Environment.NewLine + $"Product price for product '{ci.ProductId}' was NOT found ! Reason: '{productPriceResult.Message}'";
                    }
                }
            }
            else 
            {
                message += Environment.NewLine + $"No products were found for trolley '{trolley.TrolleyId}' for user '{trolley.UserId}'";
            }


            if (result.TrolleyProducts.Any())
                await _trolleyBusinessLogic.GetCatalogueProductsIntoTrolley(result.TrolleyProducts);

            return _resultFact.Result(result, true);
        }




        public async Task<IServiceResult<TrolleyReadDTO>> CreateTrolley(int userId)
        {
            if (await _trolleyRepo.ExistsByUserId(userId))
                return _resultFact.Result<TrolleyReadDTO>(null, false, $"Trolley with user Id: '{userId}' already EXISTS !");


            Console.WriteLine($"--> CREATING trolley '{userId}'......");


            var trolley = new Trolley_model { UserId = userId };

            trolley.TrolleyId = Guid.NewGuid();

            var result = await _trolleyRepo.CreateTrolley(trolley);

            if (result.State != EntityState.Added || _trolleyRepo.SaveChanges() < 1)
                return _resultFact.Result<TrolleyReadDTO>(null, false, $"Trolley for user '{userId}' was NOT created");

            trolley = (Trolley_model)result.Entity;

            return _resultFact.Result(_mapper.Map<TrolleyReadDTO>(trolley), true);
        }




        public async Task<IServiceResult<TrolleyReadDTO>> DeleteTrolley(int userId)
        {
            var trolley = await _trolleyRepo.GetTrolleyByUserId(userId);

            if(trolley == null)
                return _resultFact.Result<TrolleyReadDTO>(null, false, $"Trolley '{userId}' NOT found !");


            var message = string.Empty;

            Console.WriteLine($"--> DELETING trolley for user '{userId}'......");


            var result = await _trolleyRepo.DeleteTrolley(trolley);

            if (result.State != EntityState.Deleted || _trolleyRepo.SaveChanges() <= 0)
                return _resultFact.Result<TrolleyReadDTO>(null, false, $"Trolley with id '{userId}' was NOT removed from DB !");

            trolley = (Trolley_model)result.Entity;


            Console.WriteLine($"--> ADDING user '{userId}' trolley products' amount back to stock ......");


            foreach (var ci in trolley.TrolleyProducts)
            {
                var updateStockAmountResult = await _trolleyBusinessLogic.AddAmountToStock(ci.ProductId, ci.Amount);

                if (!updateStockAmountResult.Status)
                    message += Environment.NewLine + $"Failed to restore amount '{ci.Amount}' into stock for product '{ci.ProductId}' ! Reason: '{updateStockAmountResult.Message}'";
            }

            return _resultFact.Result(_mapper.Map<TrolleyReadDTO>(trolley), true, message);
        }





        public async Task<IServiceResult<bool>> ExistsTrolleyByTrolleyId(Guid trolleyId)
        {
            var message = "";

            var trolleyExists = await _trolleyRepo.ExistsByTrolleyId(trolleyId);

            if(!trolleyExists)
                message = $"Trolley '{trolleyId}' does NOT exist !";

            return _resultFact.Result(true, true, message);
        }



    }
}
