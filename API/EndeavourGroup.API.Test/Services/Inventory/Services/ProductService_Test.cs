using AutoMapper;
using Business.Inventory.DTOs.Product;
using Business.Libraries.ServiceResult;
using Inventory.Services;
using Inventory.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Services.Inventory.Data.Repositories.Interfaces;
using Services.Inventory.Models;



namespace EndeavourGroup.API.Test.Services.Inventory.Services
{
    [TestFixture]
    internal class ProductService_Test
    {
        private IProductService _productService;

        private Mock<IProductRepository> _repo = new Mock<IProductRepository>();
        private Mock<IMapper> _mapper = new Mock<IMapper>();

        private int _product1_Id = 1, _product2_Id = 2, _product3_Id = 3, _newProduct_Id = 4, _productToUpdate_Id = 1;
        private Product _product1, _product2, _product3, _newProduct, _productToUpdate, _productToDelete;
        private ProductReadDTO _product1ReadDTO, _product2ReadDTO, _product3ReadDTO, _newProductReadDTO, _updatedProductReadDTO, _deletedProductReadDTO;
        private ProductCreateDTO _productToCreateDTO = new ProductCreateDTO { 
            Name = "Product To Create", Notes = "Notes of Product to create", PhotoURL = "Photo URL of Product to create" 
        };
        private ProductUpdateDTO _productToUpdateDTO = new ProductUpdateDTO { 
            Name = "Product To Update", Notes = "Notes of Product to update", PhotoURL = "Photo URL of Product to update" 
        };
        private List<Product> _products;
        private IEnumerable<ProductReadDTO> _productReadDTO_List;


        [SetUp]
        public void Setup()
        {
            _product1 = new Product{ Id = _product1_Id, Name = "Product 1", Notes = "Notes of Product 1", PhotoURL = "photo url of Product 1", Archived = false};
            _product2 = new Product{ Id = _product2_Id, Name = "Product 2", Notes = "Notes of Product 2", PhotoURL = "photo url of Product 2", Archived = true};
            _product3 = new Product{ Id = _product3_Id, Name = "Product 3", Notes = "Notes of Product 3", PhotoURL = "photo url of Product 3", Archived = true};
            _products = new List<Product> { _product1, _product2, _product3};

            _product1ReadDTO = new ProductReadDTO { Id = _product1.Id, Name = _product1.Name, Notes = _product1.Notes, PhotoURL = _product1.PhotoURL };
            _product2ReadDTO = new ProductReadDTO { Id = _product2.Id, Name = _product2.Name, Notes = _product2.Notes, PhotoURL = _product2.PhotoURL };
            _product3ReadDTO = new ProductReadDTO { Id = _product3.Id, Name = _product3.Name, Notes = _product3.Notes, PhotoURL = _product3.PhotoURL };
            _productReadDTO_List = new List<ProductReadDTO> { _product1ReadDTO, _product2ReadDTO, _product3ReadDTO };

            _newProduct = new Product { Id = _newProduct_Id, Name = _productToCreateDTO.Name, Notes = _productToCreateDTO.Notes, PhotoURL = _productToCreateDTO.PhotoURL};
            _newProductReadDTO = new ProductReadDTO { Id = _newProduct.Id, Name = _newProduct.Name, Notes = _newProduct.Notes, PhotoURL = _newProduct.PhotoURL };

            _productToUpdate = new Product { Id = _productToUpdate_Id, Name = _productToUpdateDTO.Name, Notes = _productToUpdateDTO.Notes, PhotoURL = _productToUpdateDTO.PhotoURL };
            _updatedProductReadDTO = new ProductReadDTO { Id = _productToUpdate.Id, Name = _productToUpdate.Name, Notes = _productToUpdate.Notes, PhotoURL = _productToUpdate.PhotoURL };

            _productToDelete = _product1;
            _deletedProductReadDTO = new ProductReadDTO { Id = _productToDelete.Id, Name = _productToDelete.Name, Notes = _productToDelete.Notes, PhotoURL = _productToDelete.PhotoURL };


            _productService = new ProductService(_repo.Object, _mapper.Object, new ServiceResultFactory());
        }




        //  GetProducts()

        [Test]
        public void GetProducts_WhenCalled_ReturnsProducts()
        {
            _repo.Setup(r => r.GetProducts(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(_products.AsEnumerable()));

            _mapper.Setup(m => m.Map<IEnumerable<ProductReadDTO>>(It.IsAny<IEnumerable<Product>>())).Returns(_productReadDTO_List);


            var result = _productService.GetProducts().Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Count(), Is.EqualTo(_productReadDTO_List.Count()));
            Assert.That(result.Data.ElementAt(2), Is.EqualTo(_productReadDTO_List.ElementAt(2)));
        }


        [Test]
        public void GetProducts_WhenCalledWithParameter_ReturnsProducts()
        {
            IEnumerable<int> productIdsArgument = new List<int> { _product1.Id, _product2.Id };
            IEnumerable<Product> productsFromRepo = new List<Product> { _product1, _product2 };
            IEnumerable<ProductReadDTO> productReadDTOsFromRepo = new List<ProductReadDTO> { _product1ReadDTO, _product2ReadDTO };

            _repo.Setup(r => r.GetProducts(productIdsArgument)).Returns(Task.FromResult(productsFromRepo));

            _mapper.Setup(m => m.Map<IEnumerable<ProductReadDTO>>(It.IsAny<IEnumerable<Product>>())).Returns(productReadDTOsFromRepo);


            var result = _productService.GetProducts().Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Count(), Is.EqualTo(productsFromRepo.Count()));
            Assert.That(result.Data.ElementAt(1).Id, Is.EqualTo(productsFromRepo.ElementAt(1).Id));
        }


        [Test]
        public void GetProducts_NoProductsFound_ReturnsFailMessage()
        {
            _repo.Setup(r => r.GetProducts(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(new List<Product>().AsEnumerable()));

            _mapper.Setup(m => m.Map<IEnumerable<ProductReadDTO>>(It.IsAny<IEnumerable<Product>>())).Returns(It.IsAny<IEnumerable<ProductReadDTO>>());


            var result = _productService.GetProducts().Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Is.EqualTo("NO products found !"));
        }



        //  GetProductById()

        [Test]
        public void GetProductById_WhenCalled_ReturnsProduct()
        {
            _repo.Setup(r => r.GetProductById(It.IsAny<int>())).Returns(Task.FromResult(_product1));

            _mapper.Setup(m => m.Map<ProductReadDTO>(It.IsAny<Product>())).Returns(_product1ReadDTO);


            var result = _productService.GetProductById(It.IsAny<int>()).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Id, Is.EqualTo(_product1_Id));
        }


        [Test]
        public void GetProductById_ProductNotFound_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductById(It.IsAny<int>())).Returns(Task.FromResult(It.IsAny<Product>()));

            _mapper.Setup(m => m.Map<ProductReadDTO>(It.IsAny<Product>())).Returns(It.IsAny<ProductReadDTO>());


            var result = _productService.GetProductById(_product1_Id).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product '{_product1_Id}' was NOT found !"));
        }


        //  AddProduct()

        [Test]
        public void AddProduct_WhenCalled_AddsProductToRepo()
        {
            _repo.Setup(r => r.ExistsByName(_productToCreateDTO.Name)).Returns(Task.FromResult(false));

            _repo.Setup(r => r.AddProduct(It.IsAny<Product>())).Returns(Task.FromResult(EntityState.Added));

            _repo.Setup(r => r.SaveChanges()).Returns(1);

            _mapper.Setup(m => m.Map<ProductReadDTO>(It.IsAny<Product>())).Returns(_newProductReadDTO);


            var result = _productService.AddProduct(_productToCreateDTO).Result;


            _repo.Verify(r => r.AddProduct(It.IsAny<Product>()));
            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Id, Is.EqualTo(_newProduct.Id));
            Assert.That(result.Data.Name, Is.EqualTo(_productToCreateDTO.Name));
        }


        [Test]
        public void AddProduct_AlreadyExists_ReturnMessage()
        {
            _repo.Setup(r => r.ExistsByName(_productToCreateDTO.Name)).Returns(Task.FromResult(true));


            var result = _productService.AddProduct(_productToCreateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product '{_productToCreateDTO.Name}' already EXISTS !"));
        }


        [Test]
        public void AddProduct_ProductNotCreated_ReturnMessage()
        {
            _repo.Setup(r => r.ExistsByName(_productToCreateDTO.Name)).Returns(Task.FromResult(false));

            _repo.Setup(r => r.AddProduct(It.IsAny<Product>())).Returns(Task.FromResult(EntityState.Unchanged));
            // OR:
            _repo.Setup(r => r.SaveChanges()).Returns(0);


            var result = _productService.AddProduct(_productToCreateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product '{_productToCreateDTO.Name}' was NOT created"));
        }



        //  UpdateProduct()

        [Test]
        public void UpdateProduct_WhenCalled_UpdatesProduct()
        {
            _repo.Setup(r => r.GetProductById(_productToUpdate_Id)).Returns(Task.FromResult(_product1));

            _repo.Setup(r => r.ExistsByName(_productToUpdateDTO.Name)).Returns(Task.FromResult(false));

            _repo.Setup(r => r.SaveChanges()).Returns(1);

            _mapper.Setup(m => m.Map<Product>(_productToUpdateDTO)).Returns(_product1);

            _mapper.Setup(m => m.Map<ProductReadDTO>(_product1)).Returns(_updatedProductReadDTO);


            var result = _productService.UpdateProduct(_productToUpdate_Id, _productToUpdateDTO).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Id, Is.EqualTo(_productToUpdate_Id));
        }


        [Test]
        public void UpdateProduct_ProductNotFound_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductById(_productToUpdate_Id)).Returns(Task.FromResult(It.IsAny<Product>()));


            var result = _productService.UpdateProduct(_productToUpdate_Id, _productToUpdateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product '{_productToUpdate_Id}' NOT found !"));
        }


        [Test]
        public void UpdateProduct_ProductNameAlreadyExists_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductById(_productToUpdate_Id)).Returns(Task.FromResult(_product1));

            _repo.Setup(r => r.ExistsByName(_productToUpdateDTO.Name)).Returns(Task.FromResult(true));


            var result = _productService.UpdateProduct(_productToUpdate_Id, _productToUpdateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product with name: '{_productToUpdateDTO.Name}' already exists !"));
        }


        [Test]
        public void UpdateProduct_UpdateNotSaved_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductById(_productToUpdate_Id)).Returns(Task.FromResult(_product1));

            _repo.Setup(r => r.ExistsByName(_productToUpdateDTO.Name)).Returns(Task.FromResult(false));

            _repo.Setup(r => r.SaveChanges()).Returns(0);


            var result = _productService.UpdateProduct(_productToUpdate_Id, _productToUpdateDTO).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product '{_productToUpdate_Id}': changes were NOT saved into DB !"));
        }



        //  DeleteProduct()

        [Test]
        public void DeleteProduct_WhenCalled_DeletesProduct()
        {
            _repo.Setup(r => r.GetProductById(_product1_Id)).Returns(Task.FromResult(_product1));

            _repo.Setup(r => r.DeleteProduct(_product1)).Returns(Task.FromResult(EntityState.Deleted));
            // OR:
            _repo.Setup(r => r.SaveChanges()).Returns(1);

            _mapper.Setup(m => m.Map<ProductReadDTO>(_product1)).Returns(_deletedProductReadDTO);


            var result = _productService.DeleteProduct(_product1_Id).Result;


            Assert.IsTrue(result.Status);
            Assert.That(result.Data.Id, Is.EqualTo(_product1_Id));
        }



        [Test]
        public void DelteProduct_ProductNotFound_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductById(_product1_Id)).Returns(Task.FromResult(It.IsAny<Product>()));


            var result = _productService.DeleteProduct(_product1_Id).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product '{_product1_Id}' NOT found !"));
        }


        [Test]
        public void DelteProduct_ProductIsArchived_ReturnsMessage()
        {
            _product1.Archived = true;

            _repo.Setup(r => r.GetProductById(_product1_Id)).Returns(Task.FromResult(_product1));


            var result = _productService.DeleteProduct(_product1_Id).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product '{_product1_Id}' can NOT be deleted because it is ARCHIVED !"));
        }


        [Test]
        public void DelteProduct_ProductNotDeleted_ReturnsMessage()
        {
            _repo.Setup(r => r.GetProductById(_product1_Id)).Returns(Task.FromResult(_product1));

            _repo.Setup(r => r.DeleteProduct(_product1)).Returns(Task.FromResult(EntityState.Unchanged));
            // OR:
            _repo.Setup(r => r.SaveChanges()).Returns(0);

            _mapper.Setup(m => m.Map<ProductReadDTO>(_product1)).Returns(_deletedProductReadDTO);


            var result = _productService.DeleteProduct(_product1_Id).Result;


            Assert.IsFalse(result.Status);
            Assert.That(result.Message, Is.EqualTo($"Product with id '{_product1_Id}' was NOT removed from DB !"));
        }
    }
}
