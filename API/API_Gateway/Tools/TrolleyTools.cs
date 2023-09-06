using API_Gateway.Tools.Interfaces;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Trolley.DTOs;



namespace API_Gateway.Tools
{
    public class TrolleyTools : ITrolleyTools
    {



        public void ApplyTrolleyPromotionDiscount(TrolleyReadDTO trolley, IEnumerable<TrolleyPromotionReadDTO> activePromotions)
        {

            if (trolley != null
                && trolley.TrolleyProducts.Any()
                && activePromotions != null 
                && activePromotions.Any())
            {
                foreach (var tp in trolley.TrolleyProducts)
                {
                    var promotion = activePromotions.FirstOrDefault(ap => ap.ProductId == tp.ProductId);

                    if(promotion != null)
                        CalculateTrolleyPromotionDiscount(tp, promotion);

                }
            }

        }




        private void CalculateTrolleyPromotionDiscount(TrolleyProductReadDTO product, TrolleyPromotionReadDTO promotion)
        {

            if (product != null
                && promotion != null
                && promotion.TrolleyPromotionType != null)
            {
                switch (promotion.TrolleyPromotionType.TrolleyPromotionTypeId)
                {
                    case 1:
                        // Buy one and get one free

                        var freeProductAmount = (product.Amount % 2 == 0) ? product.Amount / 2 : (product.Amount - 1) / 2;

                        product.ProductTotal = (product.Amount - freeProductAmount) * product.ProductDiscountedPrice;

                        break;
                    case 2:
                        // Buy one get second one for half price

                        var halfPricedProductAmount = (product.Amount % 2 == 0) ? product.Amount / 2 : (product.Amount - 1) / 2;

                        product.ProductTotal = (product.Amount - (halfPricedProductAmount / 2)) * product.ProductDiscountedPrice;

                        break;
                    case 3:
                        // Spend and save

                        product.ProductTotal = (product.ProductDiscountedPrice * product.Amount) - (((product.Amount * product.ProductDiscountedPrice) / 100) * promotion.DiscountPercent);

                        break;
                }

            }

        }


    }
}
