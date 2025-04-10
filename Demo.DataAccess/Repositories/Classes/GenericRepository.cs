using Demo.DataAccess.Contexts;
using Demo.DataAccess.Repositories.Interface;
using Demo.DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //CRUD Operations
        // GetAll
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return dbContext.Set<TEntity>().ToList();
            else
                return dbContext.Set<TEntity>().AsNoTracking().ToList();
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

    }
}
