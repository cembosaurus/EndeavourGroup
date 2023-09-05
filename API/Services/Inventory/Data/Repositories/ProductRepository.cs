using Business.Data.Repositories;
using Services.Inventory.Models;
using Microsoft.EntityFrameworkCore;
using Services.Inventory.Data.Repositories.Interfaces;



namespace Services.Inventory.Data
{
    public class ProductRepository : BaseRepository<InventoryContext>, IProductRepository
    {

        private readonly InventoryContext _context;


        public ProductRepository(InventoryContext context): base(context)
        {
            _context = context;
        }





        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }




        public async Task<IEnumerable<Product>> GetProducts(IEnumerable<int> ProductIds = default)
        {
            if (ProductIds != null && ProductIds.Any())
                return await _context.Products
                    .Where(i => i.Archived == false && ProductIds.Contains(i.Id))
                    .ToListAsync();

            return await _context.Products.ToListAsync();
        }       
        
        
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Where(i => i.Archived == false).FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products.Where(i => i.Archived == false).FirstOrDefaultAsync(i => i.Name == name);
        }


        public async Task<EntityState> AddProduct(Product Product)
        {
            var result = await _context.Products.AddAsync(Product);

            return result.State;
        }

        public async Task<EntityState> DeleteProduct(Product Product)
        {
            return _context.Products.Remove(Product).State;
        }


        public async Task<bool> ExistsById(int id)
        { 
            return await _context.Products.Where(i => i.Archived == false).AnyAsync(i => i.Id == id);
        }


        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Products.Where(i => i.Archived == false).AnyAsync(i => i.Name == name);
        }


    }
}
