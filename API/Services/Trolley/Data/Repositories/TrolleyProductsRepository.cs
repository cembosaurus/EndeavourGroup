using Business.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Trolley.Models;
using Trolley.Data.Repositories.Interfaces;



namespace Trolley.Data.Repositories
{
    public class TrolleyProductsRepository : BaseRepository<TrolleyContext>, ITrolleyProductsRepository
    {

        private readonly TrolleyContext _context;


        public TrolleyProductsRepository(TrolleyContext context) : base(context)
        {
            _context = context;
        }




        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }



        public async Task<IEnumerable<TrolleyProduct>> GetTrolleyProducts(Guid trolleyId)
        {
            return await _context.TrolleyProducts.Where(ci => ci.TrolleyId == trolleyId).ToListAsync();
        }



        public async Task<IEnumerable<TrolleyProduct>> GetTrolleyProductsByUserId(int userId)
        {
            return await _context.Trolleys.Where(c => c.UserId == userId)
                .Select(c => c.TrolleyProducts.Where(ci => ci.TrolleyId == c.TrolleyId))
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<TrolleyProduct>> GetTrolleyProductsByTrolleyId(Guid trolleyId)
        {
            return await _context.Trolleys.Where(c => c.TrolleyId == trolleyId)
                .Select(c => c.TrolleyProducts.Where(ci => ci.TrolleyId == c.TrolleyId))
                .FirstOrDefaultAsync();
        }



        public async Task<EntityEntry> DeleteTrolleyProduct(TrolleyProduct trolleyProduct)
        {
            return _context.TrolleyProducts.Remove(trolleyProduct);
        }


        public async Task DeleteTrolleyProducts(IEnumerable<TrolleyProduct> trolleyProducts)
        {
            _context.TrolleyProducts.RemoveRange(trolleyProducts);
        }

    }
}
