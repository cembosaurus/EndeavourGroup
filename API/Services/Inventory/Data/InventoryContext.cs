using Services.Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Inventory.Data
{
    public class InventoryContext: DbContext
    {

        public InventoryContext(DbContextOptions<InventoryContext> opt) : base(opt)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CatalogueProduct> CatalogueProducts { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }     



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Product>()
                .HasOne(i => i.CatalogueProduct)
                .WithOne(ci => ci.Product);



            modelBuilder.Entity<CatalogueProduct>()
                .HasKey(ci => ci.ProductId);            
            
            modelBuilder.Entity<CatalogueProduct>()
                .HasOne(ci => ci.Product)
                .WithOne(i => i.CatalogueProduct)
                .HasPrincipalKey<Product>(i => i.Id);



            modelBuilder.Entity<ProductPrice>()
                .HasKey(ip => ip.ProductId);

            modelBuilder.Entity<ProductPrice>()
                .HasOne(ip => ip.CatalogueProduct)
                .WithOne(ci => ci.ProductPrice)
                .HasPrincipalKey<CatalogueProduct>(ci => ci.ProductId)
                .IsRequired();



            base.OnModelCreating(modelBuilder);
        }
    }
}
