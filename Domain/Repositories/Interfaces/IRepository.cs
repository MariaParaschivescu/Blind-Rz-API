using System.Linq.Expressions;
using System.Linq;

namespace Domain.Repositories.Interfaces
{
    public interface IRepository<TEntity>: IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAllQuery();
        IQueryable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById<TId>(TId id);
        TEntity SingleOrDefault(Func<TEntity, bool> predicate);

        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        Task<bool> Delete(Guid id);
        //Task<int> SaveChangesAsync();

    }
}
