using Promotion.Engine.API.Application.Commands;
using System.Threading.Tasks;

namespace Promotion.Engine.API.Application.Engine
{
    public abstract class PromotionHandler
    {
        private PromotionHandler _nextHandler;

        public PromotionHandler NextHandler(PromotionHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return _nextHandler;
        }

        public virtual Task<int> RunAsync(Request item) => _nextHandler.RunAsync(item);
    }
}
