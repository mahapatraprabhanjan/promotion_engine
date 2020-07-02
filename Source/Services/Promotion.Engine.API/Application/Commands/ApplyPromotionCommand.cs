﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Promotion.Engine.API.Application.Commands
{
    public class ApplyPromotionCommand
    {
        [JsonProperty]
        public List<CartItems> CartItems { get; private set; }

        [JsonConstructor]
        public ApplyPromotionCommand(List<CartItems> cartItems)
        {
            CartItems = cartItems;
        }
    }
}
