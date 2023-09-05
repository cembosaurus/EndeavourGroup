using Business.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Inventory.Data.Repositories.Interfaces;
using Services.Inventory.Models;



namespace Services.Inventory.Data
{

    public class CatalogueProductRepository : BaseRepository<InventoryContext>, ICatalogueProductRepository
    {

        private readonly InventoryContext _context;


        public CatalogueProductRepository(InventoryContext context) : base(context)
        {
            _context = context;
        }





        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }




        // CatalogueProduct can fetch nested Product (Name. PhotoUrl etc...) to avoid unnecessary DI of Product repo in CatalogueProduct services !




        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Name == name);
        }


        public async Task<IEnumerable<CatalogueProduct>> GetCatalogueProducts(IEnumerable<int> ProductIds = default)
        {
            if (ProductIds != null && ProductIds.Any())
                return await _context.CatalogueProducts
                    .Where(ci => ProductIds.Contains(ci.ProductId))
                    .Include(ci => ci.Product)
                    .Include(ci => ci.ProductPrice)
                    .ToListAsync();

            return await _context.CatalogueProducts
                .Include(ci => ci.Product)
                .Include(ci => ci.ProductPrice)
                .ToListAsync();
        }


        public async Task<CatalogueProduct> GetCatalogueProductById(int id)
        {
            return await _context.CatalogueProducts.Where(x => x.ProductId == id)
                .Include(ci => ci.Product)
                .Include(ci => ci.ProductPrice)
                .FirstOrDefaultAsync();
        }


        public async Task<CatalogueProduct> GetCatalogueProductWithExtrasById(int id)
        {
            return await _context.CatalogueProducts.Where(x => x.ProductId == id)
                .Include(ci => ci.Product)
                .Include(ci => ci.ProductPrice)
                .SingleOrDefaultAsync();
        }



        public async Task<EntityState> CreateCatalogueProduct(CatalogueProduct catalogueProduct)
        {
            var resultState = await _context.CatalogueProducts.AddAsync(catalogueProduct);

            return resultState.State;
        }



        public async Task<EntityState> RemoveCatalogueProduct(CatalogueProduct catalogueProduct)
        {
            return _context.CatalogueProducts.Remove(catalogueProduct).State;
        }



        public async Task<bool> ExistsById(int id)
        {
            return await _context.CatalogueProducts.AnyAsync(ci => ci.ProductId == id);
        }



        public async Task<bool> ExistsByName(string name)
        {
            return await _context.CatalogueProducts.AnyAsync(ci => ci.Product.Name == name);
        }




        public async Task<int> GetInstockCount(int id)
        {
            return await _context.CatalogueProducts.Where(ci => ci.ProductId == id).Select(s => s.Instock).FirstOrDefaultAsync();
        }

    }
}
