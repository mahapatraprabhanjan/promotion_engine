using Promotion.Engine.API.Application.Commands;
using Promotion.Engine.Domain.AggregateRoots;
using Promotion.Engine.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.Engine.API.Application.Engine
{
    public class MultipromotionHandler : PromotionHandler
    {
        private static List<string> VisitedSku = new List<string>();

        private readonly IPromotionRepository _promotionRepository;
        private readonly ISKURepository _sKURepository;

        public MultipromotionHandler(IPromotionRepository promotionRepository, ISKURepository sKURepository)
        {
            _promotionRepository = promotionRepository;
            _sKURepository = sKURepository;
            VisitedSku.Clear();
        }

        public override async Task<int> RunAsync(Request item)
        {
            var result = 0;
            var amountToSubtract = 0;
            var promotions = await _promotionRepository.GetAsync();
            var skus = await _sKURepository.GetAsync();

            if (item.PromotionType.Equals(PromotionType.MultiSku))
            {
                foreach (var sku in VisitedSku)
                {
                    var data = skus.FirstOrDefault(d => d.SKU == sku);
                    amountToSubtract += data == null ? 0 : data.Value;
                }

                VisitedSku.Add(item.Sku);
                var preparedSku =  string.Join(" & ", VisitedSku);
                var promotion = promotions.FirstOrDefault(d => d.IsActive && d.SKU.Equals(preparedSku));

                if (promotion == null)
                {
                    result += item.UnitPrice;
                }
                else
                {
                    VisitedSku.Clear();
                    result -= amountToSubtract;
                    result += promotion.Value;
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
