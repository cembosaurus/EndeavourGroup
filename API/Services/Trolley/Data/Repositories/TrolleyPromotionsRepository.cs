using Business.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Trolley.Data.Repositories.Interfaces;
using Trolley.Models;



namespace Trolley.Data.Repositories
{
    public class TrolleyPromotionsRepository : BaseRepository<TrolleyContext>, ITrolleyPromotionsRepository
    {
        private readonly TrolleyContext _context;


        public TrolleyPromotionsRepository(TrolleyContext context) : base(context)
        {
            _context = context;
        }




        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }





        public async Task<IEnumerable<TrolleyPromotion>> GetAllTrolleyPromotions()
        {
            return await _context.TrolleyPromotion.Include(t => t.TrolleyPromotionType).ToListAsync();
        }




        public async Task<IEnumerable<TrolleyPromotion>> GetActiveTrolleyPromotions()
        {
            return await _context.TrolleyPromotion.Where(t => t.IsOn).Include(t => t.TrolleyPromotionType).ToListAsync();
        }


    }
}
