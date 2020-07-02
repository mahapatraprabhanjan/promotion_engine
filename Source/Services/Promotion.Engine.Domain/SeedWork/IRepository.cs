namespace Promotion.Engine.Domain.SeedWork
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
