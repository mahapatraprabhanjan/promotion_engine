using Promotion.Engine.API.Application.Commands;
using Promotion.Engine.Domain.AggregateRoots;
using Promotion.Engine.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.Engine.API.Application.Engine
{
    public class QuantityHandler : PromotionHandler
    {
        private readonly IPromotionRepository _promotionRepository;

        public QuantityHandler(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public override async Task<int> RunAsync(Request item)
        {
            var result = 0;
            var promotions = await _promotionRepository.GetAsync();

            if (item.PromotionType.Equals(PromotionType.Quantity))
            {
                var promotion = promotions.FirstOrDefault(d => d.IsActive && d.SKU.Contains(item.Sku));
                var counts = int.Parse(promotion.SKU[0].ToString());

                if (item.Quantity >= counts)
                {
                    result += (item.Quantity / counts) * item.PromotionValue;
                    result += (item.Quantity % counts) * item.UnitPrice;
                }
                else
                {
                    result += (item.Quantity % counts) * item.UnitPrice;
                }
                return result;
            }
            else
            {
                return await base.RunAsync(item);
            }
        }
    }
}
