using Services.Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Inventory.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd, IConfiguration config)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<InventoryContext>(), isProd, config);
            }
        }


        private static void SeedData(InventoryContext context, bool isProd, IConfiguration config)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempring to apply MIGRATIONS ...");

                try
                {
                    context.Database.Migrate();

                    Console.WriteLine($"--> MIGRATIONS - applied ...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run MIGRATIONS: {ex.Message}");
                }
            }


            string Photo(string photoId) => $"products/{photoId}";

            if (!context.CatalogueProducts.Any())
            {
                Console.WriteLine("---> Seeding data ...");

                context.CatalogueProducts.AddRange(
                    new CatalogueProduct() { 
                        Product = new Product {
                            Name = "Victoria Bitter",
                            Notes = "Victoria Bitter - notes ...................",
                            PhotoURL = Photo("VictoriaBitter.jpg")
                        },
                        ProductPrice = new ProductPrice { 
                            SalePrice = (decimal)21.49,
                            RRP = (decimal)21.49,
                            DiscountPercent = 10
                        },
                        Description = "........................ description of Victoria Bitter ...........................",
                        Instock = 100
                    },
                    new CatalogueProduct() { 
                        Product = new Product {
                            Name = "Crown Lager",
                            Notes = "Crown Lager - notes ...................",
                            PhotoURL = Photo("CrownLager.jpg")
                        }, 
                        ProductPrice = new ProductPrice { 
                            SalePrice = (decimal)22.99,
                            RRP = (decimal)22.99,
                            DiscountPercent = 5
                        },
                        Description = "........................ description of Crown Lager ...........................",
                        Instock = 100
                    },
                    new CatalogueProduct() { 
                        Product = new Product {
                            Name = "Coopers",
                            Notes = "Coopers - notes ...................",
                            PhotoURL = Photo("Coopers.jpg")
                        }, 
                        ProductPrice = new ProductPrice { 
                            SalePrice = (decimal)20.49,
                            RRP = (decimal)20.49,
                            DiscountPercent = 0
                        },
                        Description = "........................ description of Coopers ...........................",
                        Instock = 100
                    },
                    new CatalogueProduct() { 
                        Product = new Product {
                            Name = "Tooheys Extra Dry",
                            Notes = "Tooheys Extra Dry - notes ...................",
                            PhotoURL = Photo("TooheysExtraDry.jpg")
                        }, 
                        ProductPrice = new ProductPrice { 
                            SalePrice = (decimal)19.99,
                            RRP = (decimal)19.99,
                            DiscountPercent = 0
                        },
                        Description = "........................ description of Tooheys Extra Dry ...........................",
                        Instock = 100
                    }
                );


                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> SEED was not performed. Data are already present.");
            }

        }

    }
}
