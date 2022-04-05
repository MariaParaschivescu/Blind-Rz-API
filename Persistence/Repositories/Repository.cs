using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Db;
        protected DbSet<TEntity> DbSet { get; }
        public Repository(DbContext dbContext)
        {
            Db = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); 
            DbSet = dbContext.Set<TEntity>();
        }
        public virtual TEntity Create(TEntity entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                return true;
            }

            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) Db.Dispose();
        }

        public virtual void Dispose()
        {     
            Dispose(true);
            GC.SuppressFinalize(this);     
        }

        public virtual IQueryable<TEntity> GetAllQuery()
        {
            return DbSet.Where(e => true);
        }

        public virtual IQueryable<TEntity> GetAllByFilter(Expression<Func<TEntity,bool>> expression)
        {
            return DbSet.Where(expression);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task<TEntity> GetById<TId>(TId id)
        {
            var result = await DbSet.FindAsync(id);
            if (result == null)
                throw new InvalidOperationException();
            return result;
        }



        //public virtual async Task<int> SaveChangesAsync()
        //{
        //    return await Db.SaveChangesAsync();
        //}

        public TEntity Update(TEntity entity)
        {
            DbSet.Update(entity);
            return entity;
        }

        public TEntity SingleOrDefault(Func<TEntity, bool> predicate) => DbSet.SingleOrDefault();

        public Task<bool> ExistsAsync<TEntity1>(Expression<Func<TEntity1, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
