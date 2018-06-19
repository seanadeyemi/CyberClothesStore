using Store.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using ClassLibrary1;

namespace Store.Logic.Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ClothDbContext dbContext;


        public Repository(ClothDbContext context)
        {
            dbContext = context;
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);

        }

        public TEntity Get(int id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
