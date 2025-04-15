using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interface;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //CRUD Operations
        // GetAll
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return dbContext.Set<TEntity>().Where(entity => entity.IsDeleted == false).ToList();
            else
                return dbContext.Set<TEntity>().Where(entity => entity.IsDeleted == false).AsNoTracking().ToList();
        }

        // GetById
        public TEntity? GetById(int id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        // Add
        public int Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            return dbContext.SaveChanges();
        }

        // Update
        public int Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return dbContext.SaveChanges();
        }

        // Delete
        public int Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            return dbContext.SaveChanges();
        }
        public IEnumerable<TResult> GetAll<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector)
        {
            return dbContext.Set<TEntity>()
                             .Select(selector).ToList();
        }
    }
}
