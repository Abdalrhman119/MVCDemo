using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interface;
using System.Linq.Expressions;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //CRUD Operations
        // GetAll
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return dbContext.Set<TEntity>().Where(entity => entity.IsDeleted != true).ToList();
            else
                return dbContext.Set<TEntity>().Where(entity => entity.IsDeleted != true).AsNoTracking().ToList();
        }
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return dbContext.Set<TEntity>().Select(selector).ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return dbContext.Set<TEntity>().Where(entity => entity.IsDeleted != true).Where(filter).ToList();
        }
        // GetById
        public TEntity? GetById(int id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        // Add
        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        // Update
        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        // Delete
        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }


    }
}
