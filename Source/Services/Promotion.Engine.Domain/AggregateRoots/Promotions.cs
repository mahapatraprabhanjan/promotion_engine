using Promotion.Engine.Domain.SeedWork;

namespace Promotion.Engine.Domain.AggregateRoots
{
    public class Promotions : Entity
    {
        private string _sku;

        private int _value;

        private PromotionType _promotionType;
        private int _promotionTypeId;

        private bool _isActive;

        public string SKU => _sku;

        public int Value => _value;

        public PromotionType PromotionType => _promotionType;

        public bool IsActive => _isActive;

        public Promotions(string sku, int Value, PromotionType promotionType, bool isActive)
        {
            _sku = sku;
            _value = Value;
            _promotionTypeId = promotionType.Id;
            _isActive = isActive;
        }
    }
}
