using Store.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace Store.Logic.Common
{
    public class MockRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IEnumerable<TEntity> _collection;


        public MockRepository()
        {
          
        }

        public void Add(TEntity entity)
        {
            _collection.ToList().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
           _collection.ToList().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.AsQueryable().Where(predicate);

        }

        public TEntity Get(int id)
        {
            return _collection.ToList()[id-1];
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _collection.ToList();
        }

        public void Remove(TEntity entity)
        {
            _collection.ToList().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _collection.ToList().RemoveRange(0, entities.Count());
        }
    }
}
