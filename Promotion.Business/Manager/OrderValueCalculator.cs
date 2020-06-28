using Promotion.Business.Extension;
using Promotion.Business.Interfaces;
using Promotion.Business.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.Business.Manager
{
    public class OrderValueCalculator : IOrderValueCalculator
    {
        private readonly IUnitPriceManager unitpriceManager;
        private readonly IPromotionManager promotionManager;
        public OrderValueCalculator(IUnitPriceManager unitpriceManager, IPromotionManager promotionManager)
        {
            this.unitpriceManager = unitpriceManager;
            this.promotionManager = promotionManager;
        }
        
        /// <summary>
        /// Calculate Order value
        /// </summary>
        /// <param name="skuUnits">Sku order units</param>
        /// <returns>Total value of order</returns>
        public async Task<int> CalculateOrder(List<SkuOrder> skuUnits)
        {
            var unitPrices = await this.unitpriceManager.GetUnitPrices();
            var promotions = await this.promotionManager.GetPromotions();

            int orderValue = 0;

            var skus = new List<SKU>() { SKU.A, SKU.B };
            var skuABIds = skuUnits.Where(c => skus.Contains(c.Id)).ToList();

            skuABIds.ForEach(skuUnitOrder =>
            {
                var unitprice = unitPrices.FirstOrDefault(c => c.Name == skuUnitOrder.Id).Value;
                var promotion = promotions.FirstOrDefault(c => !c.AllowMultiple && c.SkuUnit.Contains(skuUnitOrder.Id));
                orderValue += this.ApplyPromotion(skuUnitOrder, unitprice, promotion);
            });

            var skuCDIds = skuUnits.Except(skuABIds).ToList();

            if (skuCDIds.Count == 2)
            {
                //This is for direct calculation for C&D, applied due to time
                //Needs proper configuration on for combination of SKU Ids(ex: C&D, C&A,C&B etc)
                orderValue += promotions.FirstOrDefault(c => c.AllowMultiple).Value;
            }
            else
            {
                skuCDIds.ForEach(skuUnitOrder =>
                {
                    var unitprice = unitPrices.FirstOrDefault(c => c.Name == skuUnitOrder.Id).Value;
                    orderValue += skuUnitOrder.Quantity * unitprice;
                });
            }

            return orderValue;
        }

        /// <summary>
        /// Calucate Order value with applying promotion
        /// </summary>
        /// <param name="skuOrder"></param>
        /// <param name="defaultPriceValue"></param>
        /// <param name="promotion"></param>
        /// <returns></returns>
        private int ApplyPromotion(SkuOrder skuOrder, int defaultPriceValue, ActivePromotion promotion)
        {
            return skuOrder.Calculate(defaultPriceValue, promotion);
        }
    }
}
