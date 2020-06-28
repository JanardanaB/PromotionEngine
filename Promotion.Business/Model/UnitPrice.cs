using System;

namespace Promotion.Business.Model
{
    public class UnitPrice 
    {
        public Guid Id { get; set; }
        public SKU Name { get; set; }
        public int Value { get; set; }       
    }
}
