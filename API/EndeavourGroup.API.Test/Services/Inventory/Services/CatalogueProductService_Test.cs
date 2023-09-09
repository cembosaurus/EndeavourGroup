using AutoMapper;
using Business.Inventory.DTOs.CatalogueProduct;
using Business.Inventory.DTOs.Product;
using Business.Inventory.DTOs.ProductPrice;
using Business.Libraries.ServiceResult;
using Inventory.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Services.Inventory.Data.Repositories.Interfaces;
using Services.Inventory.Models;



namespace EndeavourGroup.API.Test.Services.Inventory.Services
{
    [TestFixture]
    public class CatalogueProductService_Test
    {
        private CatalogueProductService _catalogueProductService;

        private Mock<ICatalogueProductRepository> _catalogueProductRepo = new Mock<ICatalogueProductRepository>();
        private Mock<IProductRepository> _productRepo = new Mock<IProductRepository>();
        private Mock<IMapper> _mapper = new Mock<IMapper>();

        private int _product1_Id = 1, _product2_Id = 2, _product3_Id = 3, _product4_Id = 4, _product5_Id = 5;
        private Product _product1, _product2, _product3, _product4, _product5;
        private List<Product> _products;
        private ProductReadDTO _product1ReadDTO, _product4ReadDTO, _product5ReadDTO;
        private CatalogueProduct _catalogueProduct1, _catalogueProduct2, _catalogueProduct3, _newCatalogueProduct, _updatedCatalogueProduct;
        private List<CatalogueProduct> _catalogueProducts;
        private CatalogueProductReadDTO _catalogueProduct1ReadDTO, _catalogueProduct2ReadDTO, _catalogueProduct3ReadDTO, _newCatalogueProductReadDTO, _updatedCatalogueProductReadDTO;
        private CatalogueProductCreateDTO _catalogueProductCreateDTO;
        private CatalogueProductUpdateDTO _catalogueProductUpdateDTO;
        private List<CatalogueProductReadDTO> _catalogueProductReadDTO_List;




        [SetUp]
        public void Setup()
        {
            _product1 = new Product { Id = _product1_Id, Name = "Product 1", Notes = "Description of Product 1", PhotoURL = "photo url of Product 1", Archived = false };
            _product2 = new Product { Id = _product2_Id, Name = "Product 2", Notes = "Description of Product 2", PhotoURL = "photo url of Product 2", Archived = true };
            _product3 = new Product { Id = _product3_Id, Name = "Product 3", Notes = "Description of Product 3", PhotoURL = "photo url of Product 3", Archived = true };
            _products = new List<Product> { _product1, _product2, _product3 };
            _product4 = new Product { Id = _product4_Id, Name = "Product 4", Notes = "Description of Product 4", PhotoURL = "photo url of Product 4", Archived = true };
            _product5 = new Product { Id = _product5_Id, Name = "Product 5", Notes = "Description of Product 5", PhotoURL = "photo url of Product 5", Archived = true };

            _product1ReadDTO = new ProductReadDTO { Id = _product1.Id, Notes = _product1.Notes, Name = _product1.Name, PhotoURL = _product1.PhotoURL};

            _catalogueProduct1 = new CatalogueProduct{ ProductId = _product1.Id, Description = "Description of catalogue product 1", Instock = 11, Product = _product1};
            _catalogueProduct2 = new CatalogueProduct{ ProductId = _product2.Id, Description = "Description of catalogue product 2", Instock = 22, Product = _product2};
            _catalogueProduct3 = new CatalogueProduct{ ProductId = _product3.Id, Description = "Description of catalogue product 3", Instock = 33, Product = _product3};


            _catalogueProducts = new List<CatalogueProduct> { _catalogueProduct1, _catalogueProduct2, _catalogueProduct3};


            _catalogueProduct1ReadDTO = new CatalogueProductReadDTO { ProductId = _catalogueProduct1.ProductId, Description = _catalogueProduct1.Description, Instock = _catalogueProduct1.Instock };
            _catalogueProduct2ReadDTO = new CatalogueProductReadDTO { ProductId = _catalogueProduct2.ProductId, Description = _catalogueProduct2.Description, Instock = _catalogueProduct2.Instock };
            _catalogueProduct3ReadDTO = new CatalogueProductReadDTO { ProductId = _catalogueProduct3.ProductId, Description = _catalogueProduct3.Description, Instock = _catalogueProduct3.Instock };
            _catalogueProductReadDTO_List = new List<CatalogueProductReadDTO> { _catalogueProduct1ReadDTO, _catalogueProduct2ReadDTO, _catalogueProduct3ReadDTO };


            _catalogueProductCreateDTO = new CatalogueProductCreateDTO 
            { 
                Description = "NEW Catalogue Product 4 description", 
                ProductPrice = new ProductPriceCreateDTO 
                { 
                    SalePrice = 4000,
                    RRP = 4001,
                    DiscountPercent = 40
                }, 
                Instock = 400
            };

            _catalogueProductUpdateDTO = new CatalogueProductUpdateDTO
            {
                Description = "UPDATED Catalogue Product 4 description",
                ProductPrice = new ProductPriceUpdateDTO
                {
                    SalePrice = 5000,
                    RRP = 5001,
                    DiscountPercent = 50
                },
                Instock = 500
            };

            _newCatalogueProduct = new CatalogueProduct
            {
                ProductId = _product4.Id,
                Product = _product4,
                Description = _catalogueProductCreateDTO.Description,
                Instock = _catalogueProductCreateDTO.Instock ?? 0,
                ProductPrice = new ProductPrice
                {
                    SalePrice = _catalogueProductCreateDTO.ProductPrice.SalePrice,
                    RRP = _catalogueProductCreateDTO.ProductPrice.RRP,
                    DiscountPercent = _catalogueProductCreateDTO.ProductPrice.DiscountPercent
                }
            };

            _updatedCatalogueProduct = new CatalogueProduct
            {
                ProductId = _product5.Id,
                Product = _product5,
                Description = _catalogueProductUpdateDTO.Description,
                Instock = _catalogueProductUpdateDTO.Instock ?? 0,
                ProductPrice = new ProductPrice
                {
                    SalePrice = _catalogueProductUpdateDTO.ProductPrice.SalePrice ?? 5000,
                    RRP = _catalogueProductUpdateDTO.ProductPrice.RRP,
                    DiscountPercent = _catalogueProductUpdateDTO.ProductPrice.DiscountPercent
                }
            };

            _product4ReadDTO = new ProductReadDTO { Id = _product4.Id, Notes = _product4.Notes, Name = _product4.Name, PhotoURL = _product4.PhotoURL};
            _product5ReadDTO = new ProductReadDTO { Id = _product5.Id, Notes = _product5.Notes, Name = _product5.Name, PhotoURL = _product5.PhotoURL };

            _newCatalogueProductReadDTO = new CatalogueProductReadDTO 
            { 
                ProductId = _newCatalogueProduct.ProductId,
                Description = _newCatalogueProduct.Description,
                Instock = _newCatalogueProduct.Instock,
                Product = _product4ReadDTO,
                ProductPrice = new ProductPriceReadDTO 
                { 
                    SalePrice = _newCatalogueProduct.ProductPrice.SalePrice,
                    RRP = _newCatalogueProduct.ProductPrice.RRP,
                    DiscountPercent = _newCatalogueProduct.ProductPrice.DiscountPercent
                }
            };

            _updatedCatalogueProductReadDTO = new CatalogueProductReadDTO
            {
                ProductId = _updatedCatalogueProduct.ProductId,
                Description = _updatedCatalogueProduct.Description,
                Instock = _updatedCatalogueProduct.Instock,
                Product = _product5ReadDTO,
                ProductPrice = new ProductPriceReadDTO
                {
                    SalePrice = _updatedCatalogueProduct.ProductPrice.SalePrice,
                    RRP = _updatedCatalogueProduct.ProductPrice.RRP,
                    DiscountPercent = _updatedCatalogueProduct.ProductPrice.DiscountPercent
                }
            };

            _catalogueProductService = new CatalogueProductService(_catalogueProductRepo.Object, _productRepo.Object, _mapper.Object, new ServiceResultFactory());
        }




        //  GetCatalogueProducts()

        [Test]
        public void GetCatalogueProducts_WhenCalled_ReturnsListOfCatalogueProducts()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProducts(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(_catalogueProducts.AsEnumerable()));

            _mapper.Setup(m => m.Map<IEnumerable<CatalogueProductReadDTO>>(_catalogueProducts)).Returns(_catalogueProductReadDTO_List);


            var result = _catalogueProductService.GetCatalogueProducts(It.IsAny<IEnumerable<int>>()).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Count(), Is.EqualTo(_catalogueProducts.Count()));
            Assert.That(result.Data.ElementAt(0).ProductId, Is.EqualTo(_catalogueProducts.ElementAt(0).ProductId));
        }


        [Test]
        public void GetCatalogueProducts_CatalogueProductsNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProducts(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(new List<CatalogueProduct>().AsEnumerable()));

            _mapper.Setup(m => m.Map<IEnumerable<CatalogueProductReadDTO>>(_catalogueProducts)).Returns(It.IsAny<IEnumerable<CatalogueProductReadDTO>>());


            var result = _catalogueProductService.GetCatalogueProducts(It.IsAny<IEnumerable<int>>()).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Count(), Is.EqualTo(0));
            Assert.That(result.Message, Is.EqualTo("NO catalogue products found !"));
        }


        //  GetCatalogueProductById()

        [Test]
        public void GetCatalogueProductById_WhenCalled_ReturnsCatalogueProduct()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _mapper.Setup(m => m.Map<CatalogueProductReadDTO>(_catalogueProduct1)).Returns(_catalogueProduct1ReadDTO);


            var result = _catalogueProductService.GetCatalogueProductById(It.IsAny<int>()).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.ProductId, Is.EqualTo(_catalogueProduct1.ProductId));
        }


        [Test]
        public void GetCatalogueProductById_CatalogueProductNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));

            _productRepo.Setup(r => r.ExistsById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(false));

            _mapper.Setup(m => m.Map<CatalogueProductReadDTO>(_catalogueProduct1)).Returns(It.IsAny<CatalogueProductReadDTO>());


            var result = _catalogueProductService.GetCatalogueProductById(_catalogueProduct1.ProductId).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_catalogueProduct1.ProductId}' NOT found"));
        }


        [Test]
        public void GetCatalogueProductById_CatalogueProductNotFoundButRelatedeProductExists_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));

            _productRepo.Setup(r => r.ExistsById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(true));

            _mapper.Setup(m => m.Map<CatalogueProductReadDTO>(_catalogueProduct1)).Returns(It.IsAny<CatalogueProductReadDTO>());


            var result = _catalogueProductService.GetCatalogueProductById(_catalogueProduct1.ProductId).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_catalogueProduct1.ProductId}' NOT found\r\n, but Product with id '{_product1_Id}' exists and it's not registered in catalogue !"));
        }



        //  ExistsCatalogueProductById()

        [Test]
        public void ExistsCatalogueProductById_WhenCalled_ReturnsBoolean()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(It.IsAny<int>())).Returns(Task.FromResult(true));


            var result = _catalogueProductService.ExistsCatalogueProductById(It.IsAny<int>()).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.True);
        }


        [Test]
        public void ExistsCatalogueProductById_ProductDoesNotExist_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(false));


            var result = _catalogueProductService.ExistsCatalogueProductById(_catalogueProduct1.ProductId).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.False);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product '{_catalogueProduct1.ProductId}' does NOT exist !"));
        }





        //  CreateCatalogueProduct()

        [Test]
        public void CreateCatalogueProduct_WhenCalled_CreatesCatalogueProductInRepo()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(false));

            _productRepo.Setup(r => r.GetProductById(It.IsAny<int>())).Returns(Task.FromResult(_product4));

            _catalogueProductRepo.Setup(r => r.CreateCatalogueProduct(It.IsAny<CatalogueProduct>())).Returns(Task.FromResult(EntityState.Added));
            // OR:
            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(1);

            _mapper.Setup(m => m.Map<CatalogueProduct>(It.IsAny<Product>())).Returns(_newCatalogueProduct);

            _mapper.Setup(m => m.Map<CatalogueProduct>(It.IsAny<CatalogueProductCreateDTO>())).Returns(_newCatalogueProduct);

            _mapper.Setup(m => m.Map<CatalogueProductReadDTO>(It.IsAny<CatalogueProduct>())).Returns(_newCatalogueProductReadDTO);


            var result = _catalogueProductService.CreateCatalogueProduct(_product1.Id, _catalogueProductCreateDTO).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.ProductId, Is.EqualTo(_product4.Id));
            Assert.That(result.Data.ProductPrice.SalePrice, Is.EqualTo(_catalogueProductCreateDTO.ProductPrice.SalePrice));
            Assert.That(result.Data.Description, Is.EqualTo(_catalogueProductCreateDTO.Description));
            _catalogueProductRepo.Verify(r => r.SaveChanges());
        }


        [Test]
        public void CreateCatalogueProduct_CatalogueProductAlreadyExists_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(true));


            var result = _catalogueProductService.CreateCatalogueProduct(_product1.Id, _catalogueProductCreateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product with id '{_catalogueProduct1.ProductId}' already EXISTS !"));
        }


        [Test]
        public void CreateCatalogueProduct_ProductNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(false));

            _productRepo.Setup(r => r.GetProductById(It.IsAny<int>())).Returns(Task.FromResult(It.IsAny<Product>()));


            var result = _catalogueProductService.CreateCatalogueProduct(_product1.Id, _catalogueProductCreateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product with id '{_catalogueProduct1.ProductId}' NOT found !"));
        }


        [Test]
        public void CreateCatalogueProduct_CatalogueProductNotCreated_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(_catalogueProduct1.ProductId)).Returns(Task.FromResult(false));

            _productRepo.Setup(r => r.GetProductById(It.IsAny<int>())).Returns(Task.FromResult(_product4));

            _catalogueProductRepo.Setup(r => r.CreateCatalogueProduct(It.IsAny<CatalogueProduct>())).Returns(Task.FromResult(EntityState.Unchanged));
            // OR:
            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(0);

            _mapper.Setup(m => m.Map<CatalogueProduct>(It.IsAny<Product>())).Returns(_newCatalogueProduct);

            _mapper.Setup(m => m.Map<CatalogueProduct>(It.IsAny<CatalogueProductCreateDTO>())).Returns(_newCatalogueProduct);


            var result = _catalogueProductService.CreateCatalogueProduct(_product1.Id, _catalogueProductCreateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product with product id '{_catalogueProduct1.ProductId}' was NOT created !"));
        }





        //  UpdateCatalogueProduct()

        [Test]
        public void UpdateCatalogueProduct_WhenCalled_UpdatesCatalogueProduct()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_updatedCatalogueProduct));

            _mapper.Setup(m => m.Map<CatalogueProduct>(_catalogueProductUpdateDTO)).Returns(_updatedCatalogueProduct);

            _mapper.Setup(m => m.Map<CatalogueProductReadDTO>(_updatedCatalogueProduct)).Returns(_updatedCatalogueProductReadDTO);

            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(1);


            var result = _catalogueProductService.UpdateCatalogueProduct(_product5_Id, _catalogueProductUpdateDTO).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.ProductId, Is.EqualTo(_product5_Id));
            _catalogueProductRepo.Verify(r => r.SaveChanges());
        }


        [Test]
        public void UpdateCatalogueProduct_CatalogueProductNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));


            var result = _catalogueProductService.UpdateCatalogueProduct(_product5_Id, _catalogueProductUpdateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product with id '{_product5_Id}' NOT found !"));
        }


        [Test]
        public void UpdateCatalogueProduct_CatalogueProductNotUpdated_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_updatedCatalogueProduct));

            _mapper.Setup(m => m.Map<CatalogueProduct>(_catalogueProductUpdateDTO)).Returns(_updatedCatalogueProduct);

            _mapper.Setup(m => m.Map<CatalogueProductReadDTO>(_updatedCatalogueProduct)).Returns(_updatedCatalogueProductReadDTO);

            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(0);


            var result = _catalogueProductService.UpdateCatalogueProduct(_product5_Id, _catalogueProductUpdateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product '{_product5_Id}': changes were NOT saved into DB !"));
            _catalogueProductRepo.Verify(r => r.SaveChanges());
        }



        //  RemoveCatalogueProduct()

        [Test]
        public void RemoveCatalogueProduct_WhenCalled_RemovesCatalogueProductFromRepo()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _catalogueProductRepo.Setup(r => r.RemoveCatalogueProduct(_catalogueProduct1)).Returns(Task.FromResult(EntityState.Deleted));
            // OR:
            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(1);


            var result = _catalogueProductService.RemoveCatalogueProduct(_catalogueProduct1.ProductId).Result;


            Assert.IsTrue(result.Status);
            _catalogueProductRepo.Verify(r => r.SaveChanges());
        }


        [Test]
        public void RemoveCatalogueProduct_CatalogueProductNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));


            var result = _catalogueProductService.RemoveCatalogueProduct(_catalogueProduct1.ProductId).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product with id '{_catalogueProduct1.ProductId}' was NOT found !"));
        }


        [Test]
        public void RemoveCatalogueProduct_CatalogueProductNotRemoved_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _catalogueProductRepo.Setup(r => r.RemoveCatalogueProduct(_catalogueProduct1)).Returns(Task.FromResult(EntityState.Unchanged));
            // OR:
            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(0);

            var result = _catalogueProductService.RemoveCatalogueProduct(_catalogueProduct1.ProductId).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product with product id '{_catalogueProduct1.ProductId}' was NOT removed !"));
        }





        // GetInstockCount()

        [Test]
        public void GetInstockCount_WhenCalled_ReturnsProductStockAmount()
        {
            var _amount = 123;

            _catalogueProductRepo.Setup(r => r.ExistsById(It.IsAny<int>())).Returns(Task.FromResult(true));

            _productRepo.Setup(r => r.ExistsById(It.IsAny<int>())).Returns(Task.FromResult(true));

            _catalogueProductRepo.Setup(r => r.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_amount));


            var result = _catalogueProductService.GetInstockCount(_product1_Id).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.EqualTo(_amount));
        }


        [Test]
        public void GetInstockCount_CatalogueProductNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(_product1_Id)).Returns(Task.FromResult(false));

            _productRepo.Setup(r => r.ExistsById(It.IsAny<int>())).Returns(Task.FromResult(false));

            var result = _catalogueProductService.GetInstockCount(_product1_Id).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_product1_Id}' NOT found"));
        }


        [Test]
        public void GetInstockCount_CatalogueProductNotFoundButProductExists_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.ExistsById(It.IsAny<int>())).Returns(Task.FromResult(false));

            _productRepo.Setup(r => r.ExistsById(It.IsAny<int>())).Returns(Task.FromResult(true));


            var result = _catalogueProductService.GetInstockCount(_product1_Id).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_product1_Id}' NOT found, but Product with id '{_product1_Id}' exists and it's not registered in catalogue !"));
        }



        // RemoveFromStockAmount()

        [Test]
        public void RemoveFromStockAmount_WhenCalled_ReducesAmountCatalogueProductFromStock()
        {
            var _amount = 123;
            var _amountToRemove = 23;
            _catalogueProduct1.Instock = _amount;

            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _catalogueProductRepo.Setup(r => r.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1.Instock));

            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(1);


            var result = _catalogueProductService.RemoveFromStockAmount(_product1_Id, _amountToRemove).Result;



            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.EqualTo(_amount - _amountToRemove));
        }


        [Test]
        public void RemoveFromStockAmount_AmountToRemoveExceededAmountInStock_StockAmountIsZeroAndReturnsMessage()
        {
            var _amount = 100;
            var _amountToRemove = 123;
            _catalogueProduct1.Instock = _amount;

            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _catalogueProductRepo.Setup(r => r.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1.Instock));

            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(1);


            var result = _catalogueProductService.RemoveFromStockAmount(_product1_Id, _amountToRemove).Result;



            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.EqualTo(0));
            Assert.That(result.Message, Is.EqualTo($"Insufficient amount in stock ! Only {_amount} catalogue products were removed from stock !"));
        }


        [Test]
        public void RemoveFromStockAmount_CatalogueProductNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(_product1_Id)).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));

            _productRepo.Setup(r => r.ExistsById(_product1_Id)).Returns(Task.FromResult(false));


            var result = _catalogueProductService.RemoveFromStockAmount(_product1_Id, It.IsAny<int>()).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_product1_Id}' NOT found !"));
        }


        [Test]
        public void RemoveFromStockAmount_CatalogueProductNotFoundButProductExists_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(_product1_Id)).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));

            _productRepo.Setup(r => r.ExistsById(_product1_Id)).Returns(Task.FromResult(true));


            var result = _catalogueProductService.RemoveFromStockAmount(_product1_Id, It.IsAny<int>()).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_product1_Id}' NOT found, but Product with this id exists !"));
        }


        [Test]
        public void RemoveFromStockAmount_CatalogueProductNotRemoved_ReturnsMessage()
        {
            var _amountToRemove = 23;
            _catalogueProduct1.Instock = 123;

            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _catalogueProductRepo.Setup(r => r.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1.Instock));

            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(0);


            var result = _catalogueProductService.RemoveFromStockAmount(_product1_Id, _amountToRemove).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product '{_product1_Id}' instock amount was NOT changed !"));
        }



        // AddAmountToStock()

        [Test]
        public void AddAmountToStock_WhenCalled_AddsAmountCatalogueProductToStock()
        {
            var _amount = 100;
            var _amountToRemove = 23;
            _catalogueProduct1.Instock = _amount;

            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _catalogueProductRepo.Setup(r => r.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1.Instock));

            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(1);


            var result = _catalogueProductService.AddAmountToStock(_product1_Id, _amountToRemove).Result;



            Assert.IsTrue(result.Status);
            Assert.That(result.Data, Is.EqualTo(_amount + _amountToRemove));
        }


        [Test]
        public void AddAmountToStock_CatalogueProductNotFound_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(_product1_Id)).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));

            _productRepo.Setup(r => r.ExistsById(_product1_Id)).Returns(Task.FromResult(false));


            var result = _catalogueProductService.AddAmountToStock(_product1_Id, It.IsAny<int>()).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_product1_Id}' NOT found !"));
        }


        [Test]
        public void AddAmountToStock_CatalogueProductNotFoundButProductExists_ReturnsMessage()
        {
            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(_product1_Id)).Returns(Task.FromResult(It.IsAny<CatalogueProduct>()));

            _productRepo.Setup(r => r.ExistsById(_product1_Id)).Returns(Task.FromResult(true));


            var result = _catalogueProductService.AddAmountToStock(_product1_Id, It.IsAny<int>()).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue Product with id '{_product1_Id}' NOT found, but Product with this id exists !"));
        }


        [Test]
        public void AddAmountToStock_CatalogueProductNotRemoved_ReturnsMessage()
        {
            var _amountToRemove = 23;
            _catalogueProduct1.Instock = 100;

            _catalogueProductRepo.Setup(r => r.GetCatalogueProductById(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1));

            _catalogueProductRepo.Setup(r => r.GetInstockCount(It.IsAny<int>())).Returns(Task.FromResult(_catalogueProduct1.Instock));

            _catalogueProductRepo.Setup(r => r.SaveChanges()).Returns(0);


            var result = _catalogueProductService.AddAmountToStock(_product1_Id, _amountToRemove).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Catalogue product '{_product1_Id}' instock amount was NOT changed !"));
        }

    }
}
