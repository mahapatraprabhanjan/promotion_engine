using Promotion.Engine.Domain.AggregateRoots;
using Promotion.Engine.Domain.SeedWork;
using System.Threading.Tasks;

namespace Promotion.Engine.Domain.Repositories
{
    public interface ISKURepository : IRepository<SKUs>
    {
        Task<SKUs> GetAsync();

        Task<SKUs> GetAsync(string sku);
    }
}
