using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todo.API.DbContexts;
using Todo.API.Models;

namespace Todo.API.Repositories
{
    public class Repository<T> : IRepository<T>
     where T : BaseEntity
    {
        private readonly CourseLibraryContext _context;
        protected readonly DbSet<T> _entity;

        public Repository(CourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));;
            _entity = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }
        
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate).AsEnumerable();
        }

        public T GetById(Guid id)
        {
            return _entity.FirstOrDefault(entity => entity.Id == id);
        }
        
        public T GetOne(Expression<Func<T, bool>> predicate)
        {
            return _entity.FirstOrDefault(predicate);
        }

        public void Create(T entity)
        {
            if(entity == null) throw new ArgumentNullException($"{nameof(T)}Entity");
            _entity.Add(entity);
        }

        public void Update(T entity)
        {
            if(entity == null) throw new ArgumentNullException($"{nameof(T)}Entity");
        }

        public void Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            var entity = _entity.SingleOrDefault(s => s.Id == id);
            
            Delete(entity);
        }

        public void Delete(T entity)
        {
            if(entity == null) throw new ArgumentNullException($"{nameof(T)}Entity");
            _entity.Remove(entity);
        }

        public bool Exists(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _entity.Any(entity => entity.Id == id);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _entity.Count(predicate);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}