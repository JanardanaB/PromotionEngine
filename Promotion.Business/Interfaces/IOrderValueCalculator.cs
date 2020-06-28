using Promotion.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promotion.Business.Interfaces
{
    public interface IOrderValueCalculator
    {
        Task<int> CalculateOrder(List<SkuOrder> skuUnits);        
    }
}
