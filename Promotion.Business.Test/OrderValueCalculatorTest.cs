using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promotion.Business.Interfaces;
using Promotion.Business.Manager;
using Promotion.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promotion.Business.Test
{
    [TestClass]
    public class OrderValueCalculatorTest
    {
        private IOrderValueCalculator orderValueCalculator;

        [TestInitialize]
        public void Init()
        {
            IUnitPriceManager unitPrice = new UnitPriceManager();
            IPromotionManager promotion = new PromotionManager();
            orderValueCalculator = new OrderValueCalculator(unitPrice, promotion);
        }


        [TestMethod]
        public async Task CalculateOrder_Expected_returnValue_100()
        {
            int expectedOrderValue = 100;
            //Arrange
            List<SkuOrder> skuOrders = new List<SkuOrder>()
            {
                new SkuOrder(){Id = SKU.A,Quantity =1},
                new SkuOrder(){Id = SKU.B,Quantity =1},
                new SkuOrder(){Id = SKU.C,Quantity =1},
            };

            //Act

            var retult = await this.orderValueCalculator.CalculateOrder(skuOrders);

            //Assert
            Assert.AreEqual(expectedOrderValue, retult);
        }

        [TestMethod]
        public async Task CalculateOrder_Expected_returnValue_370()
        {
            int expectedOrderValue = 370;
            //Arrange
            List<SkuOrder> skuOrders = new List<SkuOrder>()
            {
                new SkuOrder(){Id = SKU.A,Quantity =5},
                new SkuOrder(){Id = SKU.B,Quantity =5},
                new SkuOrder(){Id = SKU.C,Quantity =1},
            };

            //Act
            var retult = await this.orderValueCalculator.CalculateOrder(skuOrders);

            //Assert
            Assert.AreEqual(expectedOrderValue, retult);
        }

        [TestMethod]
        public async Task CalculateOrder_Expected_returnValue_280()
        {
            int expectedOrderValue = 280;
            //Arrange
            List<SkuOrder> skuOrders = new List<SkuOrder>()
            {
                new SkuOrder(){Id = SKU.A,Quantity =3},
                new SkuOrder(){Id = SKU.B,Quantity =5},
                new SkuOrder(){Id = SKU.C,Quantity =1},
                new SkuOrder(){Id = SKU.D,Quantity =1},
            };

            //Act
            var retult = await this.orderValueCalculator.CalculateOrder(skuOrders);

            //Assert
            Assert.AreEqual(expectedOrderValue, retult);
        }
    }
}
