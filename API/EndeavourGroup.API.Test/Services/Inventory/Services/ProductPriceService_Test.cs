using AutoMapper;
using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult;
using Inventory.Services;
using Moq;
using NUnit.Framework;
using Services.Inventory.Data.Repositories.Interfaces;
using Services.Inventory.Models;



namespace EndeavourGroup.API.Test.Services.Inventory.Services
{
    [TestFixture]
    internal class ProductPriceService_Test
    {

        private ProductPriceService _productPriceService;
        private Mock<IProductPriceRepository> _repo = new Mock<IProductPriceRepository>();
        private Mock<IMapper> _mapper = new Mock<IMapper>();

        private int _product1_Id = 1, _product2_Id = 2, _product3_Id = 3;
        private ProductPrice _productPrice1, _productPrice2, _productPrice3, _updatedProductPrice;
        private ProductPriceReadDTO _productPrice1ReadDTO, _productPrice2ReadDTO, _productPrice3ReadDTO, _updatedProductPriceReadDTO;
        private ProductPriceUpdateDTO _productPriceUpdateDTO;
        private IEnumerable<int> _productIds;
        private IEnumerable<ProductPrice> _productPrices_List, _productPricesIncomplete_List;
        private IEnumerable<ProductPriceReadDTO> _productPriceReadDTOs_List, _productPricesIncompleteReadDTO_List;



        [SetUp]
        public void Setup()
        {
            _productIds = new List<int> { _product1_Id, _product2_Id, _product3_Id};

            _productPrice1 = new ProductPrice { ProductId = _product1_Id, SalePrice = 10, RRP = 11, DiscountPercent = 1 };
            _productPrice2 = new ProductPrice { ProductId = _product2_Id, SalePrice = 20, RRP = 21, DiscountPercent = 2 };
            _productPrice3 = new ProductPrice { ProductId = _product3_Id, SalePrice = 30, RRP = 31, DiscountPercent = 3 };
            _productPrices_List = new List<ProductPrice> { _productPrice1, _productPrice2, _productPrice3};
            _productPricesIncomplete_List = new List<ProductPrice> { _productPrice1 };

            _productPrice1ReadDTO = new ProductPriceReadDTO { ProductId = _productPrice1.ProductId, SalePrice = _productPrice1.SalePrice, RRP = _productPrice1.RRP, DiscountPercent = _productPrice1.DiscountPercent };
            _productPrice2ReadDTO = new ProductPriceReadDTO { ProductId = _productPrice2.ProductId, SalePrice = _productPrice2.SalePrice, RRP = _productPrice2.RRP, DiscountPercent = _productPrice2.DiscountPercent };
            _productPrice3ReadDTO = new ProductPriceReadDTO { ProductId = _productPrice3.ProductId, SalePrice = _productPrice3.SalePrice, RRP = _productPrice3.RRP, DiscountPercent = _productPrice3.DiscountPercent };
            _productPriceReadDTOs_List = new List<ProductPriceReadDTO> { _productPrice1ReadDTO, _productPrice2ReadDTO, _productPrice3ReadDTO };
            _productPricesIncompleteReadDTO_List = new List<ProductPriceReadDTO> { _productPrice1ReadDTO };

            _productPriceUpdateDTO = new ProductPriceUpdateDTO { SalePrice = 40, RRP = 41, DiscountPercent = 4};

            _updatedProductPrice = new ProductPrice
            {
                ProductId = _product1_Id,
                SalePrice = _productPriceUpdateDTO.SalePrice ?? 0,
                RRP = _productPriceUpdateDTO.RRP ?? 0,
                DiscountPercent = _productPriceUpdateDTO.DiscountPercent ?? 0
            };

            _updatedProductPriceReadDTO = new ProductPriceReadDTO 
            {
                ProductId = _updatedProductPrice.ProductId,
                SalePrice = _productPriceUpdateDTO.SalePrice ?? 0,
                RRP = _productPriceUpdateDTO.RRP ?? 0,
                DiscountPercent = _productPriceUpdateDTO.DiscountPercent ?? 0
            };


            _productPriceService = new ProductPriceService(_repo.Object, _mapper.Object, new ServiceResultFactory());
        }




        //  GetProductPrices()

        [Test]
        public void GetProductPrices_WhenCalled_ReturnsProductPricesList()
        {
            _repo.Setup(r => r.GetProductPrices(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(_productPrices_List));

            _mapper.Setup(m => m.Map<IEnumerable<ProductPriceReadDTO>>(_productPrices_List)).Returns(_productPriceReadDTOs_List);


            var result = _productPriceService.GetProductPrices(_productIds).Result;
        
        
            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Count(), Is.EqualTo(_productIds.Count()));
            Assert.That(result.Data.ElementAt(1).ProductId, Is.EqualTo(_productIds.ElementAt(1)));
        }


        [Test]
        public void GetProductPrices_SomeProductPricesNotFound_ReturnsProductPricesListAndMessage()
        {
            _repo.Setup(r => r.GetProductPrices(_productIds)).Returns(Task.FromResult(_productPricesIncomplete_List));

            _mapper.Setup(m => m.Map<IEnumerable<ProductPriceReadDTO>>(_productPricesIncomplete_List)).Returns(_productPricesIncompleteReadDTO_List);


            var result = _productPriceService.GetProductPrices(_productIds).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Count(), Is.EqualTo(_productPricesIncomplete_List.Count()));
            Assert.That(result.Data.ElementAt(0).ProductId, Is.EqualTo(_productIds.ElementAt(0)));
            Assert.That(result.Message, Is.EqualTo($"Prices for {_productIds.Count() - _productPricesIncomplete_List.Count()} products were not found ! Reason: Products may not be registered in catalogue."));
        }


        [Test]
        public void GetProductPrices_NoProductPricesFound_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductPrices(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(It.IsAny<IEnumerable<ProductPrice>>()));


            var result = _productPriceService.GetProductPrices(_productIds).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Is.EqualTo("NO product prices found !"));
        }



        // GetProductPriceById()

        [Test]
        public void GetProductPriceById_WhenCalled_ReturnsProductPrice()
        {
            _repo.Setup(r => r.GetProductPriceById(It.IsAny<int>())).Returns(Task.FromResult(_productPrice1));

            _mapper.Setup(m => m.Map<ProductPriceReadDTO>(_productPrice1)).Returns(_productPrice1ReadDTO);


            var result = _productPriceService.GetProductPriceById(_product1_Id).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.ProductId, Is.EqualTo(_product1_Id));
        }


        [Test]
        public void GetProductPriceById_ProductNotFound_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductPriceById(It.IsAny<int>())).Returns(Task.FromResult(It.IsAny<ProductPrice>()));

            _repo.Setup(r => r.ProductExistsById(It.IsAny<int>())).Returns(Task.FromResult(false));


            var result = _productPriceService.GetProductPriceById(_product1_Id).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product with Id '{_product1_Id}' NOT found !"));
        }


        [Test]
        public void GetProductPriceById_ProductHasNoPrice_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductPriceById(It.IsAny<int>())).Returns(Task.FromResult(It.IsAny<ProductPrice>()));

            _repo.Setup(r => r.ProductExistsById(It.IsAny<int>())).Returns(Task.FromResult(true));


            var result = _productPriceService.GetProductPriceById(_product1_Id).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product with Id '{_product1_Id}' NOT found, but Product with Id '{_product1_Id}' EXIST and is NOT labeled with price !"));
        }



        // UpdateProductPrice()

        [Test]
        public void UpdateProductPrice_WhenCalled_UpdatesProductPrice()
        {
            _repo.Setup(r => r.GetProductPriceById(It.IsAny<int>())).Returns(Task.FromResult(_productPrice1));

            _mapper.Setup(m => m.Map<ProductPrice>(_productPriceUpdateDTO)).Returns(_updatedProductPrice);

            _mapper.Setup(m => m.Map<ProductPriceReadDTO>(It.IsAny<ProductPrice>())).Returns(_updatedProductPriceReadDTO);

            _repo.Setup(r => r.SaveChanges()).Returns(1);


            var result = _productPriceService.UpdateProductPrice(_product1_Id, _productPriceUpdateDTO).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.ProductId, Is.EqualTo(_product1_Id));
            Assert.That(result.Data.SalePrice, Is.EqualTo(_productPriceUpdateDTO.SalePrice));
            Assert.That(result.Data.RRP, Is.EqualTo(_productPriceUpdateDTO.RRP));
            Assert.That(result.Data.DiscountPercent, Is.EqualTo(_productPriceUpdateDTO.DiscountPercent));
        }


        [Test]
        public void UpdateProductPrice_ProductPriceNotFound_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductPriceById(It.IsAny<int>())).Returns(Task.FromResult(It.IsAny<ProductPrice>()));


            var result = _productPriceService.UpdateProductPrice(_product1_Id, _productPriceUpdateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product price '{_product1_Id}': NOT found !"));
        }


        [Test]
        public void UpdateProductPrice_UpdateNotSaved_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductPriceById(It.IsAny<int>())).Returns(Task.FromResult(_productPrice1));

            _repo.Setup(r => r.SaveChanges()).Returns(0);


            var result = _productPriceService.UpdateProductPrice(_product1_Id, _productPriceUpdateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product price '{_product1_Id}': changes were NOT saved into DB !"));
        }


    }
}
