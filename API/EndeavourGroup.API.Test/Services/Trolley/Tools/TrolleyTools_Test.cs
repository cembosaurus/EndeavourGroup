using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using Moq;
using NUnit.Framework;
using Services.Inventory.Models;
using Services.Trolley.Models;
using Trolley.HttpServices.Interfaces;
using Trolley.Tools;



namespace EndeavourGroup.API.Test.Services.Trolley.Tools
{
    [TestFixture]
    public class TrolleyTools_Test
    {
        private TrolleyTools _trolleyTools;

        private Mock<IHttpInventoryService> _httpInventoryService = new Mock<IHttpInventoryService>();
        private IServiceResultFactory _resultFact = new ServiceResultFactory();

        private Trolley_model _trolley;
        private TrolleyProduct _trolleyProduct1, _trolleyProduct2, _trolleyProduct3;
        private Guid _trolleyId = new Guid();
        private decimal _trolleyTotal;
        private int _product1_Id = 1, _product2_Id = 2, _product3_Id = 3;
        private int _product1_CountInStock = 100;
        private ProductPrice _productPrice1, _productPrice2, _productPrice3;
        private ProductPriceReadDTO _productPrice1ReadDTO, _productPrice2ReadDTO, _productPrice3ReadDTO;
        private IEnumerable<TrolleyProduct> _trolleyProducts_List;


        [SetUp]
        public void Setup()
        {
            _trolleyProduct1 = new TrolleyProduct { TrolleyId = _trolleyId, ProductId = _product1_Id, Amount = 10};
            _trolleyProduct2 = new TrolleyProduct { TrolleyId = _trolleyId, ProductId = _product2_Id, Amount = 20};
            _trolleyProduct3 = new TrolleyProduct { TrolleyId = _trolleyId, ProductId = _product3_Id, Amount = 30};
            _trolleyProducts_List = new List<TrolleyProduct> { _trolleyProduct1, _trolleyProduct2, _trolleyProduct3 };

            _trolley = new Trolley_model
            {
                TrolleyId = _trolleyId,
                TrolleyProducts = _trolleyProducts_List.ToList()
            };

            _productPrice1 = new ProductPrice { ProductId = _product1_Id, SalePrice = 100, RRP = 101, DiscountPercent = 10 };
            _productPrice1ReadDTO = new ProductPriceReadDTO 
            { 
                ProductId = _productPrice1 .ProductId, 
                SalePrice = _productPrice1.SalePrice, 
                RRP = _productPrice1.RRP, 
                DiscountPercent = _productPrice1.DiscountPercent 
            };
            _productPrice2 = new ProductPrice { ProductId = _product2_Id, SalePrice = 200, RRP = 202, DiscountPercent = 20 };
            _productPrice2ReadDTO = new ProductPriceReadDTO
            {
                ProductId = _productPrice2.ProductId,
                SalePrice = _productPrice2.SalePrice,
                RRP = _productPrice2.RRP,
                DiscountPercent = _productPrice2.DiscountPercent
            };
            _productPrice3 = new ProductPrice { ProductId = _product3_Id, SalePrice = 300, RRP = 303, DiscountPercent = 30 };
            _productPrice3ReadDTO = new ProductPriceReadDTO
            {
                ProductId = _productPrice3.ProductId,
                SalePrice = _productPrice3.SalePrice,
                RRP = _productPrice3.RRP,
                DiscountPercent = _productPrice3.DiscountPercent
            };

            _trolleyTotal = 
                  _trolley.TrolleyProducts.ElementAt(0).Amount * _productPrice1.SalePrice 
                + _trolley.TrolleyProducts.ElementAt(1).Amount * _productPrice2.SalePrice 
                + _trolley.TrolleyProducts.ElementAt(2).Amount * _productPrice3.SalePrice;

            _trolleyTools = new TrolleyTools(_resultFact, _httpInventoryService.Object);
        }





        // UpdateTrolleyTotal()
        
        [Test]
        public void UpdateTrolleyTotal_WhenCalled_UpdatesTrolleyTotal()
        {
            _httpInventoryService.Setup(i => i.GetProductPriceById(_trolleyProduct1.ProductId)).Returns(Task.FromResult(_resultFact.Result(_productPrice1ReadDTO, true)));
            _httpInventoryService.Setup(i => i.GetProductPriceById(_trolleyProduct2.ProductId)).Returns(Task.FromResult(_resultFact.Result(_productPrice2ReadDTO, true)));
            _httpInventoryService.Setup(i => i.GetProductPriceById(_trolleyProduct3.ProductId)).Returns(Task.FromResult(_resultFact.Result(_productPrice3ReadDTO, true)));


            var result = _trolleyTools.UpdateTrolleyTotal(_trolley).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.EqualTo(_trolleyTotal));
        }


        [Test]
        public void UpdateTrolleyTotal_TrolleyNotUpdated_ReturnsMessage()
        {
            var message = "Test Message";

            _httpInventoryService.Setup(i => i.GetProductPriceById(_trolleyProduct1.ProductId)).Returns(Task.FromResult(_resultFact.Result(_productPrice1ReadDTO, true)));
            _httpInventoryService.Setup(i => i.GetProductPriceById(_trolleyProduct2.ProductId)).Returns(Task.FromResult(_resultFact.Result(_productPrice2ReadDTO, false, message)));
            _httpInventoryService.Setup(i => i.GetProductPriceById(_trolleyProduct3.ProductId)).Returns(Task.FromResult(_resultFact.Result(_productPrice3ReadDTO, true)));


            var result = _trolleyTools.UpdateTrolleyTotal(_trolley).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.EqualTo(_trolleyTotal - (_productPrice2ReadDTO.SalePrice * _trolley.TrolleyProducts.ElementAt(1).Amount)));
            Assert.That(result.Message, Is.EqualTo($"Trolley total was NOT updated by Product '{_trolleyProduct2.ProductId}' ! Reason: '{message}'"));
        }


        [Test]
        public void UpdateTrolleyTotal_TrolleyNotProvided_ReturnsMessage()
        {
            var result = _trolleyTools.UpdateTrolleyTotal(It.IsAny<Trolley_model>()).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Trolley was NOT provided !"));
        }


        [Test]
        public void UpdateTrolleyTotal_TrolleyProductsNotProvided_ReturnsMessage()
        {
            _trolley.TrolleyProducts = new List<TrolleyProduct>();

            var result = _trolleyTools.UpdateTrolleyTotal(_trolley).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"NO product found in trolley. Total is: '{_trolley.Total}'"));
        }



        // AddProductsToTrolley()

        [Test]
        public void AddProductsToTrolley_WhenCalled_AddsProductsToTrolley()
        {
            var newTrolleyProducts = new List<TrolleyProduct> { new TrolleyProduct { ProductId = _product1_Id, Amount = 10 } };
            var product1_AmountInTrolleyBeforeAdding = _trolley.TrolleyProducts.ElementAt(0).Amount;

            _httpInventoryService.Setup(i => i.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_resultFact.Result(_product1_CountInStock, true)));


            var result = _trolleyTools.AddProductsToTrolley(_trolley, newTrolleyProducts).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.ElementAt(0).Amount, Is.EqualTo(newTrolleyProducts.ElementAt(0).Amount));
            Assert.That(_trolley.TrolleyProducts.ElementAt(0).Amount, Is.EqualTo(product1_AmountInTrolleyBeforeAdding + newTrolleyProducts.ElementAt(0).Amount));
        }


        [Test]
        public void AddProductsToTrolley_SomeProductsNotAddedToTrolley_ReturnsMessage()
        {
            var newTrolleyProducts = new List<TrolleyProduct> { new TrolleyProduct { ProductId = _product1_Id, Amount = 10 } };
            var product1_AmountInTrolleyBeforeAdding = _trolley.TrolleyProducts.ElementAt(0).Amount;

            _httpInventoryService.Setup(i => i.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_resultFact.Result(It.IsAny<int>(), false)));


            var result = _trolleyTools.AddProductsToTrolley(_trolley, newTrolleyProducts).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Does.StartWith($"\r\nProduct '{_trolley.TrolleyProducts.ElementAt(0).ProductId}' was NOT added to trolley ! Reason:"));
        }



        [Test]
        public void AddProductsToTrolley_NotEnoughAmountInStore_ReturnsMessage()
        {
            var newTrolleyProducts = new List<TrolleyProduct> { new TrolleyProduct { ProductId = _product1_Id, Amount = _product1_CountInStock + 1 } };
            var product1_AmountInTrolleyBeforeAdding = _trolley.TrolleyProducts.ElementAt(0).Amount;

            _httpInventoryService.Setup(i => i.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_resultFact.Result(_product1_CountInStock, true)));


            var result = _trolleyTools.AddProductsToTrolley(_trolley, newTrolleyProducts).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Does.StartWith($"\r\nNot enough amount for product: '{_trolley.TrolleyProducts.ElementAt(0).ProductId}'."));
        }



        // RemoveProductsFromTrolley()

        [Test]
        public void RemoveProductsFromTrolley_WhenCalled_RemovesProductsFGromTrolley()
        {
            var trolleyProductsToRemove = new List<TrolleyProduct> { new TrolleyProduct { ProductId = _product1_Id, Amount = 1 } };
            var product1_AmountInTrolleyBeforeAdding = _trolley.TrolleyProducts.ElementAt(0).Amount;


            var result = _trolleyTools.RemoveProductsFromTrolley(_trolley, trolleyProductsToRemove).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.ElementAt(0).Amount, Is.EqualTo(trolleyProductsToRemove.ElementAt(0).Amount));
            Assert.That(_trolley.TrolleyProducts.ElementAt(0).Amount, Is.EqualTo(product1_AmountInTrolleyBeforeAdding - trolleyProductsToRemove.ElementAt(0).Amount));
        }


        [Test]
        public void RemoveProductsFromTrolley_ProductNotFoundInTrolley_ReturnsMessage()
        {
            var _nonExistingProduct = new TrolleyProduct { ProductId = 4, Amount = 4 };

            var trolleyProductsToRemove = new List<TrolleyProduct> { _nonExistingProduct };


            var result = _trolleyTools.RemoveProductsFromTrolley(_trolley, trolleyProductsToRemove).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Is.EqualTo($"\r\nProduct '{_nonExistingProduct.ProductId}' NOT found in trolley '{_trolley.UserId}'"));
        }


        [Test]
        public void RemoveProductsFromTrolley_TryingToRemoveMoreProductsThanArePresentOnTrolley_ReturnsMessage()
        {
            var tooHighAmountToRemove = _trolleyProduct1.Amount + 1;

            var trolleyProductToRemove = _trolley.TrolleyProducts.ElementAt(0).ProductId;

            var trolleyProductsToRemove = new List<TrolleyProduct> { new TrolleyProduct { ProductId = 1, Amount = tooHighAmountToRemove } };


            var result = _trolleyTools.RemoveProductsFromTrolley(_trolley, trolleyProductsToRemove).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Is.EqualTo($"\r\nAmount to remove '{tooHighAmountToRemove}' for product '{trolleyProductsToRemove.ElementAt(0).ProductId}' was higher than actual amount on trolley and '0' amount has remained, and trolley product '{trolleyProductToRemove}' was removed from trolley!"));
        }



    }
}
