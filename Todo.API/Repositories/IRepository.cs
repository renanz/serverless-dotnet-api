using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Todo.API.Repositories
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        public T GetById(Guid id);
        public T GetOne(Expression<Func<T, bool>> predicate);
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(Guid id);
        public void Delete(T entity);

        public bool Exists(Guid id);

        public int Count(Expression<Func<T, bool>> predicate);
        public void SaveChanges();
    }
}