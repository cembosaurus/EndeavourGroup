using Business.Data.Repositories;
using Services.Inventory.Models;
using Microsoft.EntityFrameworkCore;
using Services.Inventory.Data.Repositories.Interfaces;



namespace Services.Inventory.Data
{
    public class ProductPriceRepository : BaseRepository<InventoryContext>, IProductPriceRepository
    {

        private readonly InventoryContext _context;


        public ProductPriceRepository(InventoryContext context): base(context)
        {
            _context = context;
        }





        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }


        // new Price is created in CatalogueProduct repo when Product is 'promoted' to CatalogueProduct !


        public async Task<IEnumerable<ProductPrice>> GetProductPrices(IEnumerable<int> ProductIds)
        {
            if (ProductIds != null && ProductIds.Any())
                return await _context.ProductPrices
                    .Where(i => ProductIds.Contains(i.ProductId))
                    .ToListAsync();

            return await _context.ProductPrices.ToListAsync();
        }



        public async Task<ProductPrice> GetProductPriceById(int id)
        {
            return await _context.ProductPrices.FirstOrDefaultAsync(i => i.ProductId == id);
        }



        public async Task<bool> ProductExistsById(int id)
        {
            return await _context.Products.AnyAsync(i => i.Id == id);
        }

    }
}
