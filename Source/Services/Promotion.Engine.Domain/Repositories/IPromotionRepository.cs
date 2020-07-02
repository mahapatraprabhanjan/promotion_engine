using Promotion.Engine.Domain.AggregateRoots;
using Promotion.Engine.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promotion.Engine.Domain.Repositories
{
    public interface IPromotionRepository : IRepository<Promotions>
    {
        Task<List<Promotions>> GetAsync();
    }
}
