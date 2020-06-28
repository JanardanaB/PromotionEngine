using Promotion.Business.Model;

namespace Promotion.Business.Extension
{
    public static class CalculateOrderExtension
    {
        public static int Calculate(this SkuOrder skuOrder, int defaultValue, ActivePromotion promotion)
        {
            int applyPromotionQuantity = 0;
            if (skuOrder.Quantity >= promotion.Quantity)
            {
                applyPromotionQuantity = skuOrder.Quantity / promotion.Quantity;
            }

            if (applyPromotionQuantity != 0)
                return (applyPromotionQuantity * promotion.Value) + (skuOrder.Quantity - (promotion.Quantity * applyPromotionQuantity)) * defaultValue;

            return skuOrder.Quantity * defaultValue;
        }
    }
}
