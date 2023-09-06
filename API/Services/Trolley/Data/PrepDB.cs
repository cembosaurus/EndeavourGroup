using Microsoft.EntityFrameworkCore;
using Trolley.Data;
using Trolley.Models;



namespace Services.Inventory.Data
{


    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd, IConfiguration config)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<TrolleyContext>(), isProd, config);
            }
        }




        private static void SeedData(TrolleyContext context, bool isProd, IConfiguration config)
        {

            var promotionsTypesList = config.GetSection("TrolleyPromotionTypes").Get<List<TrolleyPromotionType>>();


            // set 'isProd' to FALSE for initial DB Seed:
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



            if (!context.TrolleyPromotionType.Any())
            {
                Console.WriteLine("---> Seeding data ...");

                context.TrolleyPromotion.AddRange(
                    new TrolleyPromotion()
                    {
                        IsOn = true,
                        TrolleyPromotionTypeId = 1,
                        TrolleyPromotionType = new TrolleyPromotionType
                        {
                            TrolleyPromotionTypeId = 1,
                            Description = promotionsTypesList.Find(ptl => ptl.TrolleyPromotionTypeId == 1)?.Description ?? "",
                            Name = promotionsTypesList.Find(ptl => ptl.TrolleyPromotionTypeId == 1)?.Name ?? ""
                        },
                        ProductId = 3,
                        SpendLevel = 0,
                        DiscountPercent = 0
                    },
                    new TrolleyPromotion()
                    {
                        IsOn = false,
                        TrolleyPromotionTypeId = 2,
                        TrolleyPromotionType = new TrolleyPromotionType
                        {
                            TrolleyPromotionTypeId = 2,
                            Description = promotionsTypesList.Find(ptl => ptl.TrolleyPromotionTypeId == 2)?.Description ?? "",
                            Name = promotionsTypesList.Find(ptl => ptl.TrolleyPromotionTypeId == 2)?.Name ?? ""
                        },
                        ProductId = 0,
                        SpendLevel = 0,
                        DiscountPercent = 0
                    },
                    new TrolleyPromotion()
                    {
                        IsOn = true,
                        TrolleyPromotionTypeId = 3,
                        TrolleyPromotionType = new TrolleyPromotionType
                        {
                            TrolleyPromotionTypeId = 3,
                            Description = promotionsTypesList.Find(ptl => ptl.TrolleyPromotionTypeId == 3)?.Description ?? "",
                            Name = promotionsTypesList.Find(ptl => ptl.TrolleyPromotionTypeId == 3)?.Name ?? ""
                        },
                        ProductId = 2,
                        SpendLevel = 50,
                        DiscountPercent = 7
                    }
                ); ;


                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> SEED was not performed. Data are already present.");
            }

        }

    }
}
