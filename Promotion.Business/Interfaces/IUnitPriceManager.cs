using Promotion.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promotion.Business.Interfaces
{
    public interface IUnitPriceManager
    {
        Task<IEnumerable<UnitPrice>> GetUnitPrices();
    }
}
