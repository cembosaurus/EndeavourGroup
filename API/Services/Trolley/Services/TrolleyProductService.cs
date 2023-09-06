using AutoMapper;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Services.Trolley.Models;
using Trolley.Data.Repositories.Interfaces;
using Trolley.HttpServices.Interfaces;
using Trolley.Services.Interfaces;
using Trolley.Tools.Interfaces;



namespace Trolley.Services
{
    public class TrolleyProductService : ITrolleyProductService
    {

        private readonly IHttpInventoryService _httpInventoryService;
        private readonly ITools _tools;
        private readonly ITrolleyProductsRepository _trolleyProductRepo;
        private readonly ITrolleyRepository _trolleyRepo;
        private readonly IServiceResultFactory _resultFact;
        private readonly IMapper _mapper;


        public TrolleyProductService(ITrolleyProductsRepository trolleyProductRepo, ITrolleyRepository trolleyRepo, IServiceResultFactory resultFact, IMapper mapper, ITools tools, IHttpInventoryService httpInventoryService)
        {
            _httpInventoryService = httpInventoryService;
            _tools = tools;
            _trolleyProductRepo = trolleyProductRepo;
            _trolleyRepo = trolleyRepo;
            _resultFact = resultFact;
            _mapper = mapper;
        }






        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> GetTrolleyProducts(int userId)
        {
            Console.WriteLine($"--> GETTING trolley products for user '{userId}' ......");

            var message = "";


            var trolleyProducts = await _trolleyProductRepo.GetTrolleyProductsByUserId(userId);

            if(trolleyProducts == null)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"Trolley for user '{userId}' NOT found !");

            if (!trolleyProducts.Any())
                message = $"Trolley for user '{userId}' does NOT contain any Products !";


            var result = _mapper.Map<ICollection<TrolleyProductReadDTO>>(trolleyProducts);

            await _tools.GetCatalogueProductsIntoTrolley(result);


            return _resultFact.Result(_mapper.Map<IEnumerable<TrolleyProductReadDTO>>(result), true, message);
        }




        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> AddProductsToTrolley(int userId, IEnumerable<TrolleyProductUpdateDTO> productsToAdd)              
        {
            var trolley = await _trolleyRepo.GetTrolleyByUserId(userId);

            if (trolley == null)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"Trolley '{userId}' NOT found !");


            var message = "";

            Console.WriteLine($"--> ADDING trolley products ......");


            var ids = productsToAdd.Select(i => i.ProductId);

            var catalogueProductsResult = await _httpInventoryService.GetCatalogueProducts(ids);

            if (!catalogueProductsResult.Status)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, catalogueProductsResult.Status, $"Products for trolley '{trolley.UserId}' were NOT verified ! Reason: {catalogueProductsResult.Message}");

            var matchingIds = catalogueProductsResult.Data.Select(i => i.ProductId);

            if(!matchingIds.Any())
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, catalogueProductsResult.Status, $"Products not found for user '{trolley.UserId}'");

            var mismatchingIds = ids.Except(matchingIds);

            if (mismatchingIds.Any())
                message += Environment.NewLine + $" --> Unmatched Ids: '{string.Join(",", mismatchingIds.ToArray())}' --";


            productsToAdd = productsToAdd.Where(i => matchingIds.Contains(i.ProductId));

            var trolleyProducts = _mapper.Map<IEnumerable<TrolleyProduct>>(productsToAdd);

            var addProductsToTrolleyResult = await _tools.AddProductsToTrolley(trolley, trolleyProducts);

            if (!addProductsToTrolleyResult.Status || _trolleyProductRepo.SaveChanges() < 1)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, addProductsToTrolleyResult.Status, addProductsToTrolleyResult.Message);


            Console.WriteLine($"--> REMOVING products from stock ......");


            foreach (var ci in trolleyProducts)
            {
                var addToStockResult = await _tools.RemoveAmountFromStock(ci.ProductId, ci.Amount);

                if (!addToStockResult.Status)
                    message += Environment.NewLine + $"Amount '{ci.Amount}' for product '{ci.ProductId}' was NOT removed from stock ! Reason: {addToStockResult.Message}";
            }


            Console.WriteLine($"--> UPDATING trolley total ......");


            var updateTrolleyTotal = await _tools.UpdateTrolleyTotal(trolley);

            if (!updateTrolleyTotal.Status || _trolleyProductRepo.SaveChanges() < 1)
                message += Environment.NewLine + $"Total for trolley '{trolley.UserId}' was NOT updated ! Reason: {updateTrolleyTotal.Message}";


            return _resultFact.Result(_mapper.Map<IEnumerable<TrolleyProductReadDTO>>(addProductsToTrolleyResult.Data), addProductsToTrolleyResult.Status, addProductsToTrolleyResult.Message + message);
        }





        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> RemoveProductsFromTrolley(int userId, IEnumerable<TrolleyProductUpdateDTO> productsToRemove)
        {
            var trolley = await _trolleyRepo.GetTrolleyByUserId(userId);

            if (trolley == null)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"Trolley '{userId}' NOT found !");
            if (trolley.TrolleyProducts == null || !trolley.TrolleyProducts.Any())
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"Trolley products for trolley '{userId}' NOT found !");


            var message = string.Empty;

            Console.WriteLine($"--> REMOVING trolley products from list ......");


            var ids = productsToRemove.Select(i => i.ProductId);

            var catalogueProductsResult = await _httpInventoryService.GetCatalogueProducts(ids);

            if (!catalogueProductsResult.Status)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, catalogueProductsResult.Status, $"Products for trolley '{trolley.UserId}' were NOT verified ! Reason: {catalogueProductsResult.Message}");

            var matchingIds = catalogueProductsResult.Data.Select(i => i.ProductId);

            if (!matchingIds.Any())
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, catalogueProductsResult.Status, $"Provided NO matching products to remove from trolley '{trolley.UserId}'");

            var mismatchingIds = ids.Except(matchingIds);

            if (mismatchingIds.Any())
                message += Environment.NewLine + $" --> Unmatched Ids: '{string.Join(",", mismatchingIds.ToArray())}' --";

            productsToRemove = productsToRemove.Where(i => matchingIds.Contains(i.ProductId));

            var trolleyProducts = _mapper.Map<IEnumerable<TrolleyProduct>>(productsToRemove);

            var removedTrolleyProductsResult = await _tools.RemoveProductsFromTrolley(trolley, _mapper.Map<IEnumerable<TrolleyProduct>>(productsToRemove));

            if (!removedTrolleyProductsResult.Status || _trolleyRepo.SaveChanges() < 1)
                return _resultFact.Result(_mapper.Map<IEnumerable<TrolleyProductReadDTO>>(removedTrolleyProductsResult.Data), false, removedTrolleyProductsResult.Message);

            message += Environment.NewLine + removedTrolleyProductsResult.Message;

            foreach (var ci in removedTrolleyProductsResult.Data)
            {
                var addToStockResult = await _tools.AddAmountToStock(ci.ProductId, ci.Amount);

                if (!addToStockResult.Status)
                    message += Environment.NewLine + $"Amount for product '{ci.ProductId}' was NOT returned into stock ! Reason: {addToStockResult.Message}";
            }


            Console.WriteLine($"--> UPDATING trolley 'Total' ......");


            var updateTrolleyTotalResult = await _tools.UpdateTrolleyTotal(trolley);

            if (!updateTrolleyTotalResult.Status || _trolleyProductRepo.SaveChanges() < 1)
                message += Environment.NewLine + $"Total in trolley '{trolley.UserId}' was NOT updated ! Reason: {updateTrolleyTotalResult.Message}";


            Console.WriteLine($"--> REMOVING trolley products from scheduler tasks (lock) ......");


            return _resultFact.Result(_mapper.Map<IEnumerable<TrolleyProductReadDTO>>(removedTrolleyProductsResult.Data), true, message);
        }




        public async Task<IServiceResult<IEnumerable<TrolleyProductReadDTO>>> DeleteProductsFromTrolley(int userId, IEnumerable<int> productIds)
        {
            var trolley = await _trolleyRepo.GetTrolleyByUserId(userId);

            if (trolley == null)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"Trolley '{userId}' NOT found !");
            if (trolley.TrolleyProducts == null || !trolley.TrolleyProducts.Any())
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"Trolley '{userId}' doesn't have products !");



            var message = string.Empty;

            Console.WriteLine($"--> REMOVING trolley products from trolley '{trolley.TrolleyId}' for user '{trolley.UserId}' ......");



            var productsToDelete = trolley.TrolleyProducts.Where(ci => productIds.Contains(ci.ProductId)).ToList();

            if(!productsToDelete.Any())
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"NO matching products found to delete from trolley '{trolley.TrolleyId}' for user '{trolley.UserId}'");

            await _trolleyProductRepo.DeleteTrolleyProducts(productsToDelete);

            var mismatchingIds = productIds.Except(productsToDelete.Select(itd => itd.ProductId));

            var saveResult = _trolleyProductRepo.SaveChanges();

            if (saveResult < 1)
                return _resultFact.Result<IEnumerable<TrolleyProductReadDTO>>(null, false, $"Trolley products were NOT removed !");
            else if (saveResult < productIds.Count())
                message += Environment.NewLine + $" - {productIds.Count() - saveResult} of the trolley products were NOT removed from trolley '{trolley.TrolleyId}' ! Products: '{string.Join(",", mismatchingIds)}'";


            Console.WriteLine($"--> UPDATING trolley total ......");


            var updateTrolleyTotalResult = await _tools.UpdateTrolleyTotal(trolley);

            if (!updateTrolleyTotalResult.Status || _trolleyProductRepo.SaveChanges() < 1)
                message += Environment.NewLine + $"Total in trolley '{trolley.UserId}' was NOT updated ! Reason: {updateTrolleyTotalResult.Message}";


            foreach (var ci in productsToDelete)
            {
                Console.WriteLine($"--> ADDING amount for product '{ci.ProductId}' into stock ......");

                var addAmountToStockResult = await _tools.AddAmountToStock(ci.ProductId, ci.Amount);

                if (!addAmountToStockResult.Status)
                    message += Environment.NewLine + $"Amount for product '{ci.ProductId}' was NOT returned into stock ! Reson: '{addAmountToStockResult.Message}'";
            }


            return _resultFact.Result(_mapper.Map<IEnumerable<TrolleyProductReadDTO>>(productsToDelete), true, message);
        }

    }
}
