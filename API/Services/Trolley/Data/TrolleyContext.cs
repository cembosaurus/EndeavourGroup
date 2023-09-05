using Microsoft.EntityFrameworkCore;
using Services.Trolley.Models;
using Trolley.Models;

namespace Trolley.Data
{
    public class TrolleyContext : DbContext
    {

        public TrolleyContext(DbContextOptions<TrolleyContext> opt) : base(opt)
        {

        }



        public DbSet<Trolley_model> Trolleys { get; set; }
        public DbSet<TrolleyProduct> TrolleyProducts { get; set; }
        public DbSet<TrolleyPromotion> TrolleyPromotion { get; set; }
        public DbSet<TrolleyPromotionType> TrolleyPromotionType { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Trolley_model>()
                .HasKey(c => c.TrolleyId);

            modelBuilder.Entity<Trolley_model>()
                .Property(c => c.TrolleyId)
                .ValueGeneratedNever();
            


            modelBuilder.Entity<TrolleyProduct>()
                .HasKey(ci => new { ci.TrolleyId, ci.ProductId });



            modelBuilder.Entity<TrolleyPromotion>()
                .HasKey(p => p.TrolleyPromotionTypeId);

            modelBuilder.Entity<TrolleyPromotion>()
                .Property(p => p.TrolleyPromotionTypeId)
                .ValueGeneratedNever();



            modelBuilder.Entity<TrolleyPromotionType>()
                .HasKey(pt => pt.TrolleyPromotionTypeId);

            modelBuilder.Entity<TrolleyPromotionType>()
                .HasOne(pt => pt.TrolleyPromotion)
                .WithOne(p => p.TrolleyPromotionType)
                .HasPrincipalKey<TrolleyPromotion>(p => p.TrolleyPromotionTypeId)
                .IsRequired();



        }

    }
}
