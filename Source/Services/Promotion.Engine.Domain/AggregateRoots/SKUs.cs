using Promotion.Engine.Domain.SeedWork;

namespace Promotion.Engine.Domain.AggregateRoots
{
    public class SKUs: Entity
    {
        private string _sku;

        private int _value;

        public string SKU => _sku;

        public int Value => _value;

        public SKUs(string sku, int value)
        {
            _sku = sku;
            _value = value;
        }
    }
}
