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
            var multiPart = string.Empty;
            var promotions = await _promotionRepository.GetAsync();
            var skus = await _sKURepository.GetAsync();
            foreach (var item in cartItems)
            {
                if(promotions.Any(d=> d.IsActive && d.SKU.Contains(item.SKU) && d.PromotionType.Equals(PromotionType.Quantity)))
                {
                    var sku = skus.FirstOrDefault(d => d.SKU == item.SKU);
                    if(sku == null)
                    {
                        // throw exception catch in HttpGlobalExceptionFilter
                    }
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
                /// This logic is absolutely bad. In Aggregate level the promotion table has to span to store different types of promotions and its SKUs
                else if(promotions.Any(d => d.IsActive && d.SKU.Contains(item.SKU) && d.PromotionType.Equals(PromotionType.MultiSku)) 
                    && string.IsNullOrWhiteSpace(multiPart))
                {
                    multiPart = item.SKU;
                }
                else if (promotions.Any(d => d.IsActive && d.SKU.Contains(item.SKU) && d.PromotionType.Equals(PromotionType.MultiSku))
                    && !string.IsNullOrWhiteSpace(multiPart))
                {
                    multiPart = multiPart + " & " + item.SKU;

                    var promotion = promotions.FirstOrDefault(d => d.IsActive && d.SKU.Contains(multiPart) && d.PromotionType.Equals(PromotionType.MultiSku));

                    result += promotion.Value;

                    multiPart = string.Empty;
                }
            }

            if(!string.IsNullOrWhiteSpace(multiPart))
            {
                var sku = skus.FirstOrDefault(d => d.SKU == multiPart);
                if (sku == null)
                {
                    // throw exception catch in HttpGlobalExceptionFilter
                }

                result += sku.Value;
            }

            return result;
        }
    }
}
