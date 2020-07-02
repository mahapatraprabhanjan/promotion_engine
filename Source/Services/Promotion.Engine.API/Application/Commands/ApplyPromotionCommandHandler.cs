using MediatR;
using Promotion.Engine.Domain.AggregateRoots;
using Promotion.Engine.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Promotion.Engine.API.Application.Commands
{
    public class ApplyPromotionCommandHandler : IRequestHandler<ApplyPromotionCommand, int>
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly ISKURepository _sKURepository;

        public ApplyPromotionCommandHandler(IPromotionRepository promotionRepository, ISKURepository sKURepository)
        {
            _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));
            _sKURepository = sKURepository ?? throw new ArgumentNullException(nameof(sKURepository));
        }

        public async Task<int> Handle(ApplyPromotionCommand request, CancellationToken cancellationToken)
        {
            var cartItems = request.CartItems;

            var result = await ProcessPromotions(cartItems);
            return result;
        }

        private async Task<int> ProcessPromotions(List<CartItems> cartItems)
        {
            var result = 0;
            var promotions = await _promotionRepository.GetAsync();
            foreach (var item in cartItems)
            {
                if(promotions.Any(d=> d.IsActive && d.SKU.Contains(item.SKU) && d.PromotionType.Equals(PromotionType.Quantity)))
                {
                    var sku = await _sKURepository.GetAsync(item.SKU);
                    var promotion = promotions.FirstOrDefault(d => d.IsActive && d.SKU.Contains(item.SKU) && d.PromotionType.Equals(PromotionType.Quantity));
                    var counts = int.Parse(promotion.SKU[0].ToString());

                    if(item.Quantity >= counts)
                    {
                        result += (item.Quantity / counts) * promotion.Value;
                        result += (item.Quantity % counts) * sku.Value;
                    }
                    else
                    {
                        result += (item.Quantity % counts) * sku.Value;
                    }
                }
            }

            return result;
        }
    }
}
