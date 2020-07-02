using Newtonsoft.Json;

namespace Promotion.Engine.API.Application.Commands
{
    public class CartItems
    {
        [JsonProperty]
        public string SKU { get; private set; }

        [JsonProperty]
        public int Quantity { get; private set; }

        [JsonConstructor]
        public CartItems(string sku, int quantity)
        {
            SKU = sku;
            Quantity = quantity;
        }

    }
}
