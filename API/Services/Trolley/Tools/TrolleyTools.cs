using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;
using Services.Trolley.Models;
using Trolley.HttpServices.Interfaces;
using Trolley.Tools.Interfaces;



namespace Trolley.Tools
{
    public class TrolleyTools : ITools
    {

        private readonly IHttpInventoryService _httpInventoryService;
        private readonly IServiceResultFactory _resultFact;


        public TrolleyTools(IServiceResultFactory resultFact, IHttpInventoryService httpInventoryService)
        {
            _httpInventoryService = httpInventoryService;
            _resultFact = resultFact;
        }






        public async Task<IServiceResult<decimal>> UpdateTrolleyTotal(Trolley_model trolley)
        {
            if (trolley == null)
                return _resultFact.Result<decimal>(0, false, $"Trolley was NOT provided !");
            if (!trolley.TrolleyProducts.Any())
                return _resultFact.Result(trolley.Total, false, $"NO product found in trolley. Total is: '{trolley.Total}'");


            var message = "";


            trolley.Total = 0;

            foreach (var ci in trolley.TrolleyProducts)
            {
                var priceResult = await _httpInventoryService.GetProductPriceById(ci.ProductId);

                if (priceResult == null || !priceResult.Status)
                {
                    message += $"Trolley total was NOT updated by Product '{ci.ProductId}' ! Reason: '{priceResult?.Message}'";

                    continue;
                }

                var price = priceResult.Data.SalePrice;

                trolley.Total += price * ci.Amount;
            }

            return _resultFact.Result(trolley.Total, true, message);
        }




        public async Task<IServiceResult<IEnumerable<TrolleyProduct>>> AddProductsToTrolley(Trolley_model trolley, IEnumerable<TrolleyProduct> source)
        {
            if (trolley == null)
                return _resultFact.Result<IEnumerable<TrolleyProduct>>(null, false, $"Trolley for update was not provided !");
            if (source == null || !source.Any())
                return _resultFact.Result<IEnumerable<TrolleyProduct>>(null, false, $"Products to update trolley were not provided !");


            var message = "";

            var result = new List<TrolleyProduct>();


            foreach (var productToAdd in source)
            {
                var inStockResult = await _httpInventoryService.GetInstockCount(productToAdd.ProductId);

                if (inStockResult == null || !inStockResult.Status)
                {
                    message += Environment.NewLine + $"Product '{productToAdd.ProductId}' was NOT added to trolley ! Reason: '{inStockResult?.Message}'";

                    continue;
                }

                if (inStockResult.Data < productToAdd.Amount)
                {
                    productToAdd.Amount = !inStockResult.Status ? 0 : inStockResult.Data;

                    message += Environment.NewLine + $"Not enough amount for product: '{productToAdd.ProductId}'. Available amount '{(!inStockResult.Status ? inStockResult.Message : inStockResult.Data)}' was added to Trolley.";
                }


                var newTrolleyProduct = new TrolleyProduct
                {
                    TrolleyId = trolley.TrolleyId,
                    ProductId = productToAdd.ProductId,
                    Amount = productToAdd.Amount,
                };


                var productInTrolley = trolley.TrolleyProducts.FirstOrDefault(ci => ci.ProductId == productToAdd.ProductId);

                if (productInTrolley == null)
                {
                    trolley.TrolleyProducts.Add(newTrolleyProduct);
                }
                else
                {
                    productInTrolley.Amount += productToAdd.Amount;
                }

                result.Add(newTrolleyProduct);
            }

            return _resultFact.Result(result.AsEnumerable(), true, message);
        }




        public async Task<IServiceResult<IEnumerable<TrolleyProduct>>> RemoveProductsFromTrolley(Trolley_model trolley, IEnumerable<TrolleyProduct> source)
        {
            if (trolley == null)
                return _resultFact.Result<IEnumerable<TrolleyProduct>>(null, false, $"Trolley for update was not provided !");
            if (source == null || !source.Any())
                return _resultFact.Result<IEnumerable<TrolleyProduct>>(null, false, $"Products to delete from trolley were not provided !");


            var message = "";

            var removedTrolleyProduct = new TrolleyProduct();
            var removedTrolleyProducts = new List<TrolleyProduct>();


            foreach (var si in source)
            {
                var productInTrolley = trolley.TrolleyProducts.FirstOrDefault(di => di.ProductId == si.ProductId);

                if (productInTrolley == null)
                {
                    message += Environment.NewLine + $"Product '{si.ProductId}' NOT found in trolley '{trolley.UserId}'";

                    continue;
                }

                productInTrolley.Amount -= si.Amount;

                if (productInTrolley.Amount < 1)
                {
                    removedTrolleyProduct.Amount = productInTrolley.Amount + si.Amount;
                    productInTrolley.Amount = 0;
                    trolley.TrolleyProducts.Remove(productInTrolley);

                    message += Environment.NewLine + $"Amount to remove '{si.Amount}' for product '{si.ProductId}' was higher than actual amount on trolley and '0' amount has remained, and trolley product '{si.ProductId}' was removed from trolley!";
                }
                else 
                {
                    removedTrolleyProduct.Amount = si.Amount;
                }

                removedTrolleyProduct.ProductId = productInTrolley.ProductId;
                removedTrolleyProduct.TrolleyId = trolley.TrolleyId;
                removedTrolleyProducts.Add(removedTrolleyProduct);
            }

            return _resultFact.Result(removedTrolleyProducts.AsEnumerable(), true, message);
        }




        public async Task<IServiceResult<int>> AddAmountToStock(int productId, int amount)
        {
            if (productId < 1 || amount < 1)
                return _resultFact.Result(0, false, $"Product Id and Amount was not provided !");

            return await _httpInventoryService.AddAmountToStock(productId, amount);
        }




        public async Task<IServiceResult<int>> RemoveAmountFromStock(int productId, int amount)
        {
            if (productId < 1 || amount < 1)
                return _resultFact.Result(0, false, $"Product Id and Amount was not provided !");

            return await _httpInventoryService.RemoveAmountFromStock(productId, amount);
        }




        public async Task GetCatalogueProductsIntoTrolley(ICollection<TrolleyProductReadDTO> trolleyProductReadDTO)
        {
            if (!trolleyProductReadDTO.Any())
                return;


            for (int i = 0; i < trolleyProductReadDTO.Count(); i++)
            {
                var t = trolleyProductReadDTO.ElementAt(i);

                var product = await _httpInventoryService.GetCatalogueProductById(t.ProductId);
                var price = await _httpInventoryService.GetProductPriceById(t.ProductId);

                t.Name = product.Data.Product.Name;
                t.SalePrice = price.Data.SalePrice;
                t.ProductDiscountedPrice = price.Data.DiscountedPrice;
                t.ProductTotal = price.Data.DiscountedPrice * t.Amount;
            }
        }


    }
}
