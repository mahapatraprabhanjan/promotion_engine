using Promotion.Engine.Domain.SeedWork;

namespace Promotion.Engine.Domain.AggregateRoots
{
    public class PromotionType : Enumeration
    {
        public static PromotionType Quantity = new PromotionType(1, nameof(Quantity));
        public static PromotionType MultiSku = new PromotionType(2, nameof(MultiSku));
        public static PromotionType Percentage = new PromotionType(3, nameof(Percentage));

        public PromotionType(int id, string name) : base(id, name)
        {
        }
    }
}
