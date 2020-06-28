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
        private readonly IUnitPriceManager unitprice;
        private readonly IPromotionManager promotion;
        public OrderValueCalculator(IUnitPriceManager unitprice, IPromotionManager promotion)
        {
            this.unitprice = unitprice;
            this.promotion = promotion;
        }
        
        public async Task<int> CalculateOrder(List<SkuOrder> skuUnits)
        {
            var unitPrices = await this.unitprice.GetUnitPrices();
            var promotions = await this.promotion.GetPromotions();

            int orderValue = 0;

            var skus = new List<SKU>() { SKU.A, SKU.B };
            var skuABS = skuUnits.Where(c => skus.Contains(c.Id)).ToList();

            skuABS.ForEach(skuUnitOrder =>
            {
                var unitprice = unitPrices.FirstOrDefault(c => c.Name == skuUnitOrder.Id).Value;
                var promotion = promotions.FirstOrDefault(c => !c.AllowMultiple && c.SkuUnit.Contains(skuUnitOrder.Id));
                orderValue += this.ApplyPromotion(skuUnitOrder, unitprice, promotion);
            });

            var skuCds = skuUnits.Except(skuABS).ToList();

            if (skuCds.Count == 2)
            {
                orderValue += promotions.FirstOrDefault(c => c.AllowMultiple).Value;
            }
            else
            {
                skuCds.ForEach(skuUnitOrder =>
                {
                    var unitprice = unitPrices.FirstOrDefault(c => c.Name == skuUnitOrder.Id).Value;
                    orderValue += skuUnitOrder.Quantity * unitprice;
                });
            }


            return orderValue;
        }
        public int ApplyPromotion(SkuOrder skuOrder, int defaultPriceValue, ActivePromotion promotion)
        {
            return skuOrder.Calculate(defaultPriceValue, promotion);
        }
    }
}
