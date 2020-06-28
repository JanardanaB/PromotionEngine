using System;
using System.Collections.Generic;

namespace Promotion.Business.Model
{
    public class ActivePromotion
    {
        public Guid Id { get; set; }
        public IEnumerable<SKU> SkuUnit { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }
        public bool AllowMultiple { get; set; }
    }
}
