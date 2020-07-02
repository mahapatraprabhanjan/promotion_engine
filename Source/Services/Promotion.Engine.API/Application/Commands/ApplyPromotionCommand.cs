using Newtonsoft.Json;

namespace Promotion.Engine.API.Application.Commands
{
    public class ApplyPromotionCommand
    {
        [JsonProperty]
        public string SKU { get; private set; }

        [JsonProperty]
        public int Quantity { get; private set; }

        [JsonConstructor]
        public ApplyPromotionCommand(string sku, int quantity)
        {
            SKU = sku;
            Quantity = quantity;
        }
    }
}
