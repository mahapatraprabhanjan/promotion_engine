using MediatR;
using Promotion.Engine.Domain.AggregateRoots;
using System.Collections.Generic;

namespace Promotion.Engine.API.Application.Commands
{
    public class Request
    {
        public string Sku { get; private set; }

        public int Quantity { get; private set; }

        public PromotionType PromotionType { get; private set; }

        public int PromotionValue { get; private set; }

        public int UnitPrice { get; private set; }

        public Request(string sku, int quantity, PromotionType promotionType, int promotionValue = 0, int unitPrice = 0)
        {
            Sku = sku;
            Quantity = quantity;
            PromotionType = promotionType;
            PromotionValue = promotionValue;
            UnitPrice = unitPrice;
        }
    }
}
