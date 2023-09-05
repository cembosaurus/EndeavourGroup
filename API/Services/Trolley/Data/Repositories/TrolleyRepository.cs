using Business.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Trolley.Models;
using Trolley.Data.Repositories.Interfaces;



namespace Trolley.Data.Repositories
{
    public class TrolleyRepository : BaseRepository<TrolleyContext>, ITrolleyRepository
    {

        private readonly TrolleyContext _context;


        public TrolleyRepository(TrolleyContext context) : base(context)
        {
            _context = context;
        }





        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }




        public async Task<IEnumerable<Trolley_model>> GetTrolleys()
        {
            return await _context.Trolleys.Include(t => t.TrolleyProducts).ToListAsync();
        }


        public async Task<Guid> GetTrolleyIdByUserId(int userId)
        {
            return await _context.Trolleys.Where(c => c.UserId == userId).Select(c => c.TrolleyId).FirstOrDefaultAsync();
        }


        public async Task<int> GetUserIdByTrolleyId(Guid trolleyId)
        {
            return await _context.Trolleys.Where(c => c.TrolleyId == trolleyId).Select(c => c.UserId).FirstOrDefaultAsync();
        }


        public async Task<Trolley_model> GetTrolleyByUserId(int userId)
        {
            return await _context.Trolleys.Include(c => c.TrolleyProducts).FirstOrDefaultAsync(c => c.UserId == userId);
        }



        public async Task<Trolley_model> GetTrolleyByTrolleyId(Guid trolleyId)
        {
            return await _context.Trolleys.Include(c => c.TrolleyProducts).FirstOrDefaultAsync(c => c.TrolleyId == trolleyId);
        }



        public async Task<EntityEntry> CreateTrolley(Trolley_model trolley)
        {
            return await _context.Trolleys.AddAsync(trolley);
        }



        public async Task<EntityEntry> DeleteTrolley(Trolley_model trolley)
        {
            return _context.Trolleys.Remove(trolley);
        }


        public async Task<bool> ExistsByUserId(int id)
        {
            return await _context.Trolleys.AnyAsync(c => c.UserId == id);
        }


        public async Task<bool> ExistsByTrolleyId(Guid id)
        {
            return await _context.Trolleys.AnyAsync(c => c.TrolleyId == id);
        }

    }
}
